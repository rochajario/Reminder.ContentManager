namespace ContentManager.Domain.Interfaces
{
    public interface IMessagingClient<T> where T : class
    {
        void Consume(Action<T> payloadAction);
        void Publish(T payload);
    }
}
