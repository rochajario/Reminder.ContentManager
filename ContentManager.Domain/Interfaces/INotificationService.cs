using ContentManager.Domain.Models.Requests;
using ContentManager.Domain.Models.Responses;

namespace ContentManager.Domain.Interfaces
{
    public interface INotificationService
    {
        Guid AddNotification(NotificationRequest request);
        NotificationResponse GetNotificationById(Guid id);
        IEnumerable<NotificationResponse> GetNotifications();
        void RemoveNotification(Guid id);
        void UpdateNotification(Guid id, NotificationRequest request);
    }
}
