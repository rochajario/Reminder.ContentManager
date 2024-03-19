using ContentManager.Data.Entities;
using ContentManager.Data.Enums;
using ContentManager.Data.Interfaces;
using ContentManager.Domain.Interfaces;
using ContentManager.Domain.Models.Requests;
using ContentManager.Domain.Models.Responses;

namespace ContentManager.Domain.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepo;
        private readonly INotificationRepository _notificationRepo;

        public ReminderService(IReminderRepository reminderRepo, INotificationRepository notificationRepo)
        {
            _reminderRepo = reminderRepo;
            _notificationRepo = notificationRepo;
        }

        public Guid AddReminder(ReminderRequest request)
        {
            return _reminderRepo.Create(RequestToEntity(request)).Id;
        }

        public ReminderResponse GetReminderById(Guid userId, Guid reminderId)
        {
            var result = _reminderRepo.ReadById(reminderId);
            if (result is null || !result.UserId.Equals(userId))
            {
                throw new ArgumentException("Couldn't find reminder");
            }

            return EntityToResponse(result);
        }

        public IEnumerable<ReminderResponse> GetReminders(Guid userId)
        {
            return _reminderRepo
                .ReadAll()
                .Where(r => r.UserId.Equals(userId))
                .Select(x => EntityToResponse(x))
                .ToArray();
        }

        public void UpdateReminder(Guid id, ReminderRequest request)
        {
            _reminderRepo.Update(id, RequestToEntity(request));
        }

        public void RemoveReminder(Guid id)
        {
            _reminderRepo.Delete(id);
        }

        private static Reminder RequestToEntity(ReminderRequest request)
        {
            return new Reminder
            {
                UserId = request.UserId,
                Content = request.Content
            };
        }

        private static ReminderResponse EntityToResponse(Reminder entity)
        {
            return new ReminderResponse
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Content = entity.Content,
                Notifications = entity.Notifications.Select(notification => new NotificationResponse
                {
                    Id = notification.Id,
                    NotifyAt = notification.NotifyAt,
                    Status = Enum.GetName(typeof(NotificationStatus), notification.Status)!
                })
            };
        }

        public Guid AddReminderNotification(Guid reminderId, NotificationRequest notificationRequest)
        {
            return _notificationRepo.Create(new Notification
            {
                ReminderId = reminderId,
                NotifyAt = notificationRequest.NotifyAt
            }).Id;
        }
    }
}
