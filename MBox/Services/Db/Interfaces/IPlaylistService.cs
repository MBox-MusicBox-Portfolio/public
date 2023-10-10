using MBox.Models.Db;
using MBox.Models.Helpers;
using MBox.Models.Pagination;

namespace MBox.Services.Db.Interfaces;

public interface IPlaylistService : IBaseService<Playlist>
{
    Task<IEnumerable<Playlist>> GetByAuthorAsync(Guid id, PaginationInfo pagination);
    Task<Playlist> AddSongToPlaylist(Guid userId, Guid playlistId, Guid songId);
    Task<Playlist> DeleteSongFromPlaylist(Guid userId, Guid playlistId, Guid songId);
    Task<bool> FollowPlaylist(Guid userId, Guid playlistId);
    Task<bool> UnfollowPlaylist(Guid userId, Guid playlistId);
    Task<Playlist> AddAsync(PlaylistHelper playlist);
    Task<bool> DeleteAsync(Guid id, Guid userId);
    Task<Playlist> UpdateAsync(Playlist playlist, Guid userId);
}