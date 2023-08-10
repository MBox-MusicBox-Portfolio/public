using System.Text.Json;
using System.Text;
using RabbitMQ.Client;

namespace MBox.Services.RabbitMQ;

public class RabbitMQService
{
    private readonly IConnection _connection;

    public RabbitMQService(IConnection connection)
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