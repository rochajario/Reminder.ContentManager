namespace ContentManager.Domain.Models.Requests
{
    public class ReminderRequest
    {
        public Guid UserId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
