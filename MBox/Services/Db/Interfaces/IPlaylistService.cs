using MBox.Models.Db;
using MBox.Models.Pagination;

namespace MBox.Services.Db.Interfaces;

public interface IPlaylistService : IBaseService<Playlist>
{
    Task<IEnumerable<Playlist>> GetByAuthorAsync(Guid id, PaginationInfo pagination);
    Task<Playlist> AddSongToPlaylist(Guid userId, Guid playlistId, Guid songId);
    Task<Playlist> DeleteSongFromPlaylist(Guid userId, Guid playlistId, Guid songId);
}