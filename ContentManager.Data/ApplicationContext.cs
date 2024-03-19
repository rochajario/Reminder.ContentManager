using ContentManager.Data.Entities;
using ContentManager.Data.Enums;
using ContentManager.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Data
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Reminder>()
                .HasMany(x => x.Notifications)
                .WithOne();

            modelBuilder
                .Entity<Notification>()
                .Property(x => x.Status)
                .HasDefaultValue(NotificationStatus.Pending);

            modelBuilder
                .Entity<Notification>()
                .HasOne<Reminder>()
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.ReminderId);

        }
    }
}