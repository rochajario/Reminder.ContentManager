namespace ContentManager.Domain.Interfaces
{
    public interface IPublishingService
    {
        void PublishRemindersBasedOnTimeInterval(TimeSpan interval);
    }
}
