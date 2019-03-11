using RabbitMQ.Client;
using System;
using System.Text;
using RabbitMQ.Client.Events;

namespace Consumidor
{
	public class Receiver
	{
		static void Main(string[] args)
		{
			Console.Title = "RECEIVER";

			var factory = new ConnectionFactory() { HostName = "localhost" };

			using (var connection = factory.CreateConnection())
			{
				using (var channel = connection.CreateModel())
				{
					channel.QueueDeclare(queue: "WillianTest",
						durable: false,
						exclusive: false,
						autoDelete: false,
						arguments: null);

					var consumer = new EventingBasicConsumer(channel);
					consumer.Received += (model, ea) =>
					{
						var body = ea.Body;
						var message = Encoding.UTF8.GetString(body);
						Console.WriteLine(message);
					};

					channel.BasicConsume(queue: "WillianTest",
						autoAck: true,
						consumer: consumer);

					Console.WriteLine("Consumidor funcionando");
					Console.ReadLine();
				}
			}
		}
	}
}
