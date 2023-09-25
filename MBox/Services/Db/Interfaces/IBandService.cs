using MBox.Models.Db;

namespace MBox.Services.Db.Interfaces;

public interface IBandService : IBaseService<Band>
{
    Task<User> FollowBand(Guid userId, Guid bandId);
    Task<User> UnFollowBand(Guid userId, Guid bandId);
}