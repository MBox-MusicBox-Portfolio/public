using MBox.Models.Db;
using MBox.Models.Exceptions;
using MBox.Repositories.Interfaces;
using MBox.Services.Db.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MBox.Services.Db;

public class BandService : BaseService<Band>, IBandService
{
    private readonly IRepository<User> _repositoryUser;
    public BandService(IRepository<Band> repository, 
        IRepository<User> repositoryUser) 
        : base(repository)
    {
        _repositoryUser = repositoryUser;
    }

    public async Task<User> FollowBand(Guid userId, Guid bandId)
    {
        var user = await _repositoryUser.GetByIdAsync(userId);
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
            user.FollowingsBands?.Add(band);
            await _repositoryUser.UpdateAsync(user);
            return user;
        }

        throw new BadRequestException("User already follows this Band", "User");
    }

    public async Task<User> UnFollowBand(Guid userId, Guid bandId)
    {
        var user = await _repositoryUser.GetByIdAsync(userId);
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