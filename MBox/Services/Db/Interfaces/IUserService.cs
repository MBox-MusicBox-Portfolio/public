using MBox.Models.Db;
using MBox.Models.Pagination;

namespace MBox.Services.Db.Interfaces;

public interface IUserService : IBaseService<User>
{
    Task<Playlist> GetUserLibrary(Guid userId);
    Task<IEnumerable<Playlist>> GetUserPlaylists(Guid userId, PaginationInfo pagination);
    Task<IEnumerable<Playlist>> GetLikedPlaylists(Guid userId, PaginationInfo pagination);
    Task<IEnumerable<Band>> GetFollowedBands(Guid userId, PaginationInfo pagination);
}