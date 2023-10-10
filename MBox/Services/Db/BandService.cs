using MBox.Models.Db;
using MBox.Models.Exceptions;
using MBox.Models.RabbitMq;
using MBox.Repositories.Interfaces;
using MBox.Services.Db.Interfaces;
using MBox.Services.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MBox.Services.Db;

public class BandService : BaseService<Band>, IBandService
{
    private readonly IRepository<User> _repositoryUser;
    private readonly RabbitMqService _rabbit;

    public BandService(IRepository<Band> repository,
        RabbitMqService rabbit,
        IRepository<User> repositoryUser)
        : base(repository)
    {
        _repositoryUser = repositoryUser;
        _rabbit = rabbit;
    }

    public async Task<IEnumerable<Band>> GetBandByTerm(string term)
    {
        Expression<Func<Band, bool>> filter = band => band.Name.Contains(term);
        List<Band> bands = await BuildQuery(filter).ToListAsync();
        if (bands != null)
        {
            return bands;
        }
        return new List<Band>();
    }

    public async Task<User> FollowBand(Guid userId, Guid bandId)
    {
        var user = await _repositoryUser
            .BuildQuery()
            .Where(u => u.Id == userId)
            .Include(u => u.FollowingsBands)
            .FirstOrDefaultAsync();
        if (user == null)
        {
            throw new NotFoundException("Not found User", "User");
        }
        var band = user.FollowingsBands?.FirstOrDefault(b => b.Id == bandId);
        if (band == null)
        {
            band = await GetByIdAsync(bandId);
            if (band == null)
            {
                throw new NotFoundException("Not found Band", "Band");
            }
            var eventObj = new EventMessage()
            {
                From = userId.ToString(),
                Body = new
                {
                    user.Email,
                    user.Name,
                },
                Template = "follow_band",
                To = bandId.ToString()

            };
            _rabbit.SendMessage(eventObj);
            user.FollowingsBands?.Add(band);
            await _repositoryUser.UpdateAsync(user);
            return user;
        }

        throw new BadRequestException("User already follows this Band", "User");
    }

    public async Task<User> UnFollowBand(Guid userId, Guid bandId)
    {
        var user = await _repositoryUser
            .BuildQuery()
            .Where(u => u.Id == userId)
            .Include(u => u.FollowingsBands)
            .FirstOrDefaultAsync();
        if (user == null)
        {
            throw new NotFoundException("Not found User", "User");
        }
        var band = user.FollowingsBands?.FirstOrDefault(b => b.Id == bandId);
        if (band != null)
        {
            user.FollowingsBands?.Remove(band);
            await _repositoryUser.UpdateAsync(user);
            return user;
        }

        throw new BadRequestException("User does not follow this Band", "User");
    }
}