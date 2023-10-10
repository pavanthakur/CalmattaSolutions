using Calmatta.DAL.Helper;
using Calmatta.DAL.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calmatta.DAL.Services
{
    public class ChatPublisherService : IChatPublisherService
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private IConnection _connection;

        private readonly ILogger<ChatPublisherService> _logger;

        public ChatPublisherService(IOptions<RabbitMqConfiguration> rabbitMqOptions, ILogger<ChatPublisherService> logger)
        {
            _logger = logger;

            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName;
        }

        public bool Publish(string message)
        {
            if (!ConnectionExists()) return false;

            using (var channel = _connection.CreateModel())
            {
                var queue = channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var team = AvailableTeamsRepository.GetTeam();
                var teamCapacity = team.GetCapacity();

                if (queue.MessageCount == teamCapacity)
                    return false;

                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            }

            return true;
        }

        private IConnection CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not create RabbitMQ connection");
            }
            return _connection;
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }
    }
}

