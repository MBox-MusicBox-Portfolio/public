namespace AdministrationWebApi.Models.RabbitMq
{
    public class SendObject
    {
        public Guid Id { get; set; }=Guid.NewGuid();
        public string? Template { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public Object? Body { get; set; }
    }
}
