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

public class BandApplicationService : BaseService<Application>, IBandApplicationService
{
    private readonly IConfiguration _configuration;

    public BandApplicationService(
                IConfiguration configuration,
                IRepository<Application> app)
                : base(app)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<Application>> GetByStatusAsync(Guid id, PaginationInfo pagination)
    {
        Expression<Func<Application, bool>> filter = app => app.Status.Id == id;
        return await BuildQuery(filter, pagination).ToListAsync();
    }

    public async Task<IEnumerable<Application>> GetByUserAsync(Guid id, PaginationInfo pagination)
    {
        Expression<Func<Application, bool>> filter = app => app.Producer.Id == id;
        return await BuildQuery(filter, pagination).ToListAsync();
    }
}