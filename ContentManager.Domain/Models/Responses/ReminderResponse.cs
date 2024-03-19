namespace ContentManager.Domain.Models.Responses
{
    public class ReminderResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public IEnumerable<NotificationResponse> Notifications { get; set; } = Enumerable.Empty<NotificationResponse>();
        public Guid UserId { get; internal set; }
    }
}
