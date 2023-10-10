using System;
using System.Text;
using Calmatta.DAL.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Calmatta.ChatMonitorClient
{
    internal class Program
    {
        private const string QueueName = "CalmattaChatQueue";
        private const string HostName = "localhost";

        static void Main(string[] args)
        {
            Console.WriteLine("Chat coordinator started");

            var team = AvailableTeamsRepository.GetTeam();
            var agentChatCoordinatorService = new AgentChatCoordinatorService(team.Agents, team.OverflowAgents);

            var factory = new ConnectionFactory
            {
                HostName = HostName
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(QueueName, false, false, false, null);
                Console.WriteLine("Waiting for messages...");

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, eventArguments) =>
                {
                    var message = Encoding.UTF8.GetString(eventArguments.Body.ToArray());

                    Console.WriteLine($"Assigning message {message} to an agent...");

                    var assignedAgent = agentChatCoordinatorService.AssignChat(message);

                    if (assignedAgent != null)
                    {
                        Console.WriteLine($"Message {message} assigned to {assignedAgent.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Agent capacity full, Message {message} will be discarded ");
                    }

                };

                channel.BasicConsume(QueueName, true, consumer);
            }

            Console.WriteLine("Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
