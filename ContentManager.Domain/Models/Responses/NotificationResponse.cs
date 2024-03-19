namespace ContentManager.Domain.Models.Responses
{
    public class NotificationResponse
    {
        public Guid Id { get; set; }
        public DateTime NotifyAt { get; set; }
        public string Status { get; internal set; }
    }
}
