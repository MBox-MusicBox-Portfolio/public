using MBox.Models.Db;
using MBox.Models.Pagination;
using MBox.Repositories.Interfaces;
using MBox.Services.Db.Interfaces;
using MBox.Services.RabbitMQ;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MBox.Services.Db;

public class BandApplicationService : BaseService<BandApplication>, IBandApplicationService
{
    private readonly IConfiguration _configuration;
    private readonly RabbitMQService _rabbit;
    //private readonly IHubContext<NotificationSignalR> _hubContext;
    private readonly IRepository<ApplicationStatus> _repositoryStatus;
    private readonly IRepository<User> _repositoryUser;
    private readonly IRepository<Band> _repositoryBand;
    private readonly IRepository<Producer> _repositoryProducer;
    private readonly IRepository<Role> _repositoryRole;

    public BandApplicationService(RabbitMQService rabbit,
                IConfiguration configuration,
                //IHubContext<NotificationSignalR> hubContext,
                IRepository<BandApplication> app,
                IRepository<ApplicationStatus> repositoryStatus,
                IRepository<User> repositoryUser,
                IRepository<Band> repositoryBand,
                IRepository<Producer> repositoryProducer,
                IRepository<Role> repositoryRole)
                : base(app)
    {
        _rabbit = rabbit;
        //_hubContext = hubContext;
        _configuration = configuration;
        _repositoryStatus = repositoryStatus;
        _repositoryUser = repositoryUser;
        _repositoryBand = repositoryBand;
        _repositoryProducer = repositoryProducer;
        _repositoryRole = repositoryRole;
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

    public async Task<BandApplication> AddAsync(Guid id, PaginationInfo pagination)
    {
        Expression<Func<BandApplication, bool>> filter = app => app.Producer.Id == id;
        return await BuildQuery(filter, pagination).ToListAsync();
    }
}