using ContentManager.Domain.Interfaces;

namespace ContentManager.Worker
{
    public class PublisherWorker : BackgroundService
    {
        private readonly TimeSpan WaitTime = TimeSpan.FromSeconds(30);
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PublisherWorker(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(WaitTime);
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var notificationService = scope.ServiceProvider.GetService<IPublishingService>();

                if (notificationService != null)
                {
                    notificationService?.PublishRemindersBasedOnTimeInterval(WaitTime);
                }
            }
        }
    }
}