using ContentManager.Domain.Models.Requests;
using ContentManager.Domain.Models.Responses;

namespace ContentManager.Domain.Interfaces
{
    public interface IReminderService
    {
        Guid AddReminder(ReminderRequest request);
        Guid AddReminderNotification(Guid reminderId, NotificationRequest notificationRequest);
        ReminderResponse GetReminderById(Guid userId, Guid reminderId);
        IEnumerable<ReminderResponse> GetReminders(Guid userId);
        void RemoveReminder(Guid id);
        void UpdateReminder(Guid id, ReminderRequest request);
    }
}
