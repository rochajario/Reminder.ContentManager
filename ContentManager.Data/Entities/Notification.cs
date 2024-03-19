using ContentManager.Data.Enums;

namespace ContentManager.Data.Entities
{
    public class Notification : BaseModel
    {
        public Guid ReminderId { get; set; }
        public DateTime NotifyAt { get; set; }
        public NotificationStatus Status { get; set; }
    }
}
