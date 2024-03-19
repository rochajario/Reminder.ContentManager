using ContentManager.Data.Entities;
using ContentManager.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Data.Repositories
{
    public class ReminderRepository : BaseRepository<Reminder>, IReminderRepository
    {
        public ReminderRepository(IApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public override IQueryable<Reminder> ReadAll()
        {
            return _applicationContext.Reminders
                .Include(x => x.Notifications)
                .AsNoTracking();
        }

        public override Reminder ReadById(Guid id)
        {
            return _applicationContext.Reminders
                .Where(r => r.Id.Equals(id))
                .Include(x => x.Notifications)
                .AsNoTracking()
                .Single();
        }
    }
}
