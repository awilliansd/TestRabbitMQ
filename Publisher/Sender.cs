using System;
using System.Text;
using RabbitMQ.Client;

namespace TestRabbitMQ
{
	public class Sender
	{
		static void Main(string[] args)
		{
			Console.Title = "SENDER";

			var factory = new ConnectionFactory() { HostName = "localhost" };

			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: "WillianTest",
					durable: false,
					exclusive: false,
					autoDelete: false,
					arguments: null);

				string message = "Tutorial RabbitMQ!!!!!";
				var body = Encoding.UTF8.GetBytes(message);

				channel.BasicPublish(exchange: "",
					routingKey: "WillianTest",
					basicProperties: null,
					body: body);

				Console.WriteLine(" [x] Sent {0}", message);
			}

			Console.WriteLine(" Press [enter] to exit.");
			Console.ReadLine();
		}
	}
}
