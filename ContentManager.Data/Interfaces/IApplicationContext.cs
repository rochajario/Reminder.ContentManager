using ContentManager.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Data.Interfaces
{
    public interface IApplicationContext
    {
        DbSet<Reminder> Reminders { get; set; }
        DbSet<Notification> Notifications { get; set; }
    }
}
