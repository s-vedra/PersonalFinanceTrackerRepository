﻿using Microsoft.Extensions.Options;
using PFA_MBService.HelperMethods;
using PFA_MBService.ServiceProperties;
using RabbitMQ.Client;
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

            _channel.QueueDeclare(queue: _settings.UpdateBalanceQueue, durable: true, exclusive: false, autoDelete: false);
        }

        public string RecieveMessageFromUpdateBalanceQueue()
        {
            var result = _channel.BasicGet(queue: _settings.UpdateBalanceQueue, autoAck: true);
            if (result is null)
                return string.Empty;

            var body = result.Body.ToArray();
            return Encoding.UTF8.GetString(body);
        }
    }
}
