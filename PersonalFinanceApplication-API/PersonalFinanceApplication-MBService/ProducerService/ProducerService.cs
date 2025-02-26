﻿using Microsoft.Extensions.Options;
using PersonalFinanceApplication_MBService.HelperMethods;
using PersonalFinanceApplication_MBService.ServiceProperties;
using RabbitMQ.Client;
using System.Text;

namespace PersonalFinanceApplication_MBService.ProducerService
{
    public class ProducerService : IProducerService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly RabbitMQSettings _settings;

        public ProducerService(IOptions<RabbitMQSettings> options)
        {
            _settings = options.Value;
            var factory = ProducerServiceHelperMethods.GetConnectionFactory(_settings);
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _settings.UpdateBalanceQueue, durable: true, exclusive: false, autoDelete: false);
        }

        public void PublishMessageToUpdateBalanceQueue<T>(T request)
        {
            var message = request.ConvertToJson();
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: _settings.DefaultAnalyzingExchange,
                             routingKey: _settings.UpdateBalanceQueue,
                             basicProperties: null,
                             body: body);
        }
    }
}
