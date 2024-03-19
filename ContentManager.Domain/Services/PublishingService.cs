using ContentManager.Data;
using ContentManager.Data.Entities;
using ContentManager.Data.Enums;
using ContentManager.Data.Interfaces;
using ContentManager.Domain.Interfaces;
using ContentManager.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Domain.Services
{
    public class PublishingService : IPublishingService
    {
        private readonly IMessagingClient<Reminder> _messagingClient;
        private readonly IApplicationContext _applicationContext;

        public PublishingService(IApplicationContext applicationContext, IMessagingClient<Reminder> messagingClient)
        {
            _messagingClient = messagingClient;
            _applicationContext = applicationContext;
        }

        public void PublishRemindersBasedOnTimeInterval(TimeSpan interval)
        {
            var targetInterval = TimeZoneDate.Now.Add(interval);

            var reminders = (
                from r in _applicationContext.Reminders
                join n in _applicationContext.Notifications on r.Id equals n.ReminderId
                where n.NotifyAt > TimeZoneDate.Now
                where n.NotifyAt <= targetInterval
                select new Reminder
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    Content = r.Content
                })
                .AsNoTracking()
                .ToList();

            if (reminders is not null)
            {
                PublishReminders(reminders, targetInterval);
            }
        }

        private void PublishReminders(IEnumerable<Reminder> reminders, DateTime targetInterval)
        {
            var fullyLoadedReminders = LoadRemindersNotifications(reminders, targetInterval);
            fullyLoadedReminders.ToList().ForEach(_messagingClient.Publish);
        }

        private IEnumerable<Reminder> LoadRemindersNotifications(IEnumerable<Reminder> reminders, DateTime targetInterval)
        {
            using var context = (ApplicationContext)_applicationContext;

            var obtainedReminders = reminders.ToList();
            var notifications = context.Notifications
                .Where(n => n.NotifyAt > TimeZoneDate.Now)
                .Where(n => n.NotifyAt <= targetInterval)
                .Where(n => obtainedReminders.Select(x => x.Id).Contains(n.ReminderId))
                .ToList();

            foreach (var notification in notifications)
            {
                notification.Status = NotificationStatus.Selected;
            }
            context.UpdateRange(notifications);
            context.SaveChanges();

            foreach (var reminder in obtainedReminders)
            {
                reminder.Notifications = notifications
                    .Where(n => n.ReminderId.Equals(reminder.Id))
                    .ToList();
            }

            return obtainedReminders;
        }
    }
}
