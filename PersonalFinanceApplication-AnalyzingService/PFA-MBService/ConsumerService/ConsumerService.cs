using Microsoft.Extensions.Options;
using PFA_MBService.HelperMethods;
using PFA_MBService.ServiceProperties;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace PFA_MBService.ConsumerService
{
    public class ConsumerService : IConsumerService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly RabbitMQSettings _settings;

        public ConsumerService(IOptions<RabbitMQSettings> options)
        {
            _settings = options.Value;

            var factory = ConsumerServiceHelperMethods.GetConnectionFactory(_settings);
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _settings.AnalyzingQueue, durable: true, exclusive: false, autoDelete: false);
        }

        public void RecieveMessageFromAnalyzingQueue()
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
            };

            _channel.BasicConsume(queue: _settings.AnalyzingQueue,
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}
