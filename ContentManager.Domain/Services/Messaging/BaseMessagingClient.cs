using ContentManager.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ContentManager.Domain.Services.Messaging
{
    public abstract class BaseMessagingClient<T> : IMessagingClient<T> where T : class
    {
        private readonly IModel _channel;

        public BaseMessagingClient(IConfiguration configuration)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(configuration.GetConnectionString("RabbitMQ")!)
            };

            _channel = factory
                .CreateConnection()
                .CreateModel();

            InitiateQueue();
        }

        protected virtual void InitiateQueue()
        {
            _channel.QueueDeclare(
                queue: GetQueueName(),
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public virtual void Publish(T payload)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(payload));

            _channel.BasicPublish(
                exchange: string.Empty,
                routingKey: GetQueueName(),
                basicProperties: null,
                body: body);
        }

        public virtual void Consume(Action<T> payloadAction)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                var body = JsonConvert.DeserializeObject<T>(message);

                payloadAction.Invoke(body!);
            };

            _channel.BasicConsume(
                queue: GetQueueName(),
                autoAck: true,
                consumer: consumer);
        }

        protected abstract string GetQueueName();
    }
}
