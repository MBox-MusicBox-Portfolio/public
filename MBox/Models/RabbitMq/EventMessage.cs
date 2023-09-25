namespace MBox.Models.RabbitMq;

public class EventMessage
{
    public object Body { get; set; }
    public string? From { get; set; }
    public string Title { get; set; }
    public string? To { get; set; }
}