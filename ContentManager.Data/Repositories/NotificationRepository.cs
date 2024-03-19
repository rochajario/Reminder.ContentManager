using ContentManager.Data.Entities;
using ContentManager.Data.Interfaces;

namespace ContentManager.Data.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(IApplicationContext applicationContext) : base(applicationContext)
        {
        }
    }
}
