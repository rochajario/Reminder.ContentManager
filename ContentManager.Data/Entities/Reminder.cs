namespace ContentManager.Data.Entities
{
    public class Reminder : BaseModel
    {
        public Guid UserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public ICollection<Notification>? Notifications { get; set; }
    }
}
