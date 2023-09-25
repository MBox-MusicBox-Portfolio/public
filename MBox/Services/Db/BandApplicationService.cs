using MBox.Models.Db;
using MBox.Models.Pagination;
using MBox.Repositories.Interfaces;
using MBox.Services.Db.Interfaces;
using MBox.Services.RabbitMQ;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MBox.Models.RabbitMq;

namespace MBox.Services.Db;

public class BandApplicationService : BaseService<BandApplication>, IBandApplicationService
{
    private readonly IConfiguration _configuration;
    private readonly RabbitMqService _rabbit;

    public BandApplicationService(RabbitMqService rabbit,
                IConfiguration configuration,
                IRepository<BandApplication> app)
                : base(app)
    {
        _rabbit = rabbit;
        _configuration = configuration;
    }

    public async Task<IEnumerable<BandApplication>> GetByStatusAsync(Guid id, PaginationInfo pagination)
    {
        Expression<Func<BandApplication, bool>> filter = app => app.Status.Id == id;
        return await BuildQuery(filter, pagination).ToListAsync();
    }

    public async Task<IEnumerable<BandApplication>> GetByUserAsync(Guid id, PaginationInfo pagination)
    {
        Expression<Func<BandApplication, bool>> filter = app => app.Producer.Id == id;
        return await BuildQuery(filter, pagination).ToListAsync();
    }

    public override async Task<BandApplication> AddAsync(BandApplication application)
    {
        var msg = new EventMessage()
        {
            From = application.Producer?.Id.ToString(),
            Title = "application_created",
            Body = application
        };
        _rabbit.SendMessage(msg);
        return await _repository.AddAsync(application);
    }

    public override async Task<BandApplication> UpdateAsync(BandApplication application)
    {
        var msg = new EventMessage()
        {
            From = application.Producer?.Id.ToString(),
            Title = "application_updated",
            Body = application
        };
        _rabbit.SendMessage(msg);
        return await _repository.UpdateAsync(application);
    }
}