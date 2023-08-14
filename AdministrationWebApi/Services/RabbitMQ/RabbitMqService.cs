using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace AdministrationWebApi.Services.RabbitMQ
{
    public class RabbitMqService
    {
        private readonly IConnection _connection;

        public RabbitMqService(IConnection connection)
        {
            _connection = connection;
        }
        public void SendMessage(object obj, string? queue)
        {
            if (queue == null)
                return;
            var message = JsonSerializer.Serialize(obj);
            SendMessage(message, queue);
        }

        public void SendMessage(string message, string? queue)
        {
            using var channel = _connection.CreateModel();

            channel.QueueDeclare(queue: queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                routingKey: queue,
                body: body);
        }
    }
}
