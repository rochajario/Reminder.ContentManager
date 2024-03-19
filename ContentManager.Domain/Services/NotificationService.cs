using ContentManager.Data.Entities;
using ContentManager.Data.Enums;
using ContentManager.Data.Interfaces;
using ContentManager.Domain.Interfaces;
using ContentManager.Domain.Models.Requests;
using ContentManager.Domain.Models.Responses;

namespace ContentManager.Domain.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepositry;

        public NotificationService(INotificationRepository repository)
        {
            _notificationRepositry = repository;
        }

        public Guid AddNotification(NotificationRequest request)
        {
            return _notificationRepositry.Create(RequestToEntity(request)).Id;
        }

        private static NotificationResponse EntityToResponse(Notification entity)
        {
            return new NotificationResponse
            {
                Id = entity.Id,
                NotifyAt = entity.NotifyAt,
                Status = Enum.GetName(typeof(NotificationStatus), entity.Status)!
            };
        }

        public NotificationResponse GetNotificationById(Guid id)
        {
            return EntityToResponse(_notificationRepositry.ReadById(id));
        }

        public IEnumerable<NotificationResponse> GetNotifications()
        {
            return _notificationRepositry.ReadAll().Select(x => EntityToResponse(x)).ToList().AsReadOnly();
        }

        public void RemoveNotification(Guid id)
        {
            _notificationRepositry.Delete(id);
        }

        private static Notification RequestToEntity(NotificationRequest request)
        {
            return new Notification
            {
                NotifyAt = request.NotifyAt
            };
        }

        public void UpdateNotification(Guid id, NotificationRequest request)
        {
            var reminderId = _notificationRepositry.ReadById(id).ReminderId;
            _notificationRepositry.Update(id, new Notification
            {
                ReminderId = reminderId,
                NotifyAt = request.NotifyAt,
            });
        }
    }
}
