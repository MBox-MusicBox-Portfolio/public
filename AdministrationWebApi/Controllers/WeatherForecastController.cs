using AdministrationWebApi.Models.Db;
using Microsoft.AspNetCore.Mvc;
using AdministrationWebApi.Models.RabbitMq;
using AdministrationWebApi.Services.RabbitMQ;
using AdministrationWebApi.Services.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace AdministrationWebApi.Controllers
{
    [ApiController]
    [Route("/api/admin/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, RabbitMqService rabbit, IConfiguration configuration, IHubContext<NotificationSignalR> hubContext, AppDb db)
        {
            _logger = logger;
            _configuration = configuration;
            _rabbit = rabbit;
            _hubContext = hubContext;
            _db = db;
        }

        private readonly AppDb _db;
        private readonly IConfiguration _configuration;
        private readonly RabbitMqService _rabbit;
        private readonly IHubContext<NotificationSignalR> _hubContext;

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            if (_db.Users?.Count() == 0)
            {
                var role_user=_db.Roles.FirstOrDefault(r=>r.Name== "user");
                var role_admin = _db.Roles.FirstOrDefault(r => r.Name == "admin");
                var status =_db.StatusApplications.FirstOrDefault(r=>r.Name== "new");
                var user = new User()
                {
                    Name="user",
                    Email="victorgolova@gmail.com",
                    Password="password",
                    Birthday=DateTime.Now,
                    Role=role_user
                };

                var admin = new User()
                {
                    Name = "admin",
                    Email = "admin@gmail.com",
                    Password = "password",
                    Birthday = DateTime.Now,
                    Role = role_admin
                };

                var applications = new Applications()
                {
                    BandName="my band",
                    FullInfo="my full info about band",
                    Producer=user,
                    Status=status,
                };

                _db.Applications.Add(applications);
                _db.Users.Add(user);
                _db.Users.Add(admin);
                _db.SaveChanges();
            }
            var id = Guid.NewGuid();
            var msg = new SendObject()
            {
                Id=id,
                Email = "victorgolova@gmail.com",
                Template = "user_delete",
                Name="my name",
                Body = new { Name = "My name" }
            };
            _rabbit.SendMessage(msg, _configuration["Queue:MAILER"]);
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        
        [HttpGet("{id}/signal")]
        public async Task<IActionResult> GetSignalRAsync(string id)
        {
            await _hubContext.Clients.Group(id).SendAsync("ReceiveMessage", "message");
            return Ok();
        }
    }
}