using System.Linq.Expressions;
using MBox.Models.Db;
using MBox.Models.Exceptions;
using MBox.Models.Pagination;
using MBox.Repositories.Interfaces;
using MBox.Services.Db.Interfaces;
using MBox.Services.RabbitMQ;
using Microsoft.EntityFrameworkCore;

namespace MBox.Services.Db;

public class PlaylistService : BaseService<Playlist>, IPlaylistService
{
    private readonly IConfiguration _configuration;
    private readonly IRepository<Song> _repositorySong;

    public PlaylistService(IConfiguration configuration,
        IRepository<Playlist> repositoryPlaylist,
        IRepository<Song> repositorySong)
        : base(repositoryPlaylist)
    {
        _configuration = configuration;
        _repositorySong = repositorySong;
    }

    public async Task<Playlist> AddSongToPlaylist(Guid userId, Guid playlistId, Guid songId)
    {
        var playlist = await BuildQuery().Include(pl => pl.Songs).FirstOrDefaultAsync(pl => pl.Id == playlistId);
        if (playlist == null)
        {
            throw new NotFoundException("Not found Playlist", "Playlist");
        }

        if (playlist.Author.Id != userId)
        {
            throw new InsufficientRightsException("Only author can edit this playlist", "Playlist");
        }

        var tmpSong = playlist.Songs.FirstOrDefault(s => s.Id == songId);
        if (tmpSong == null)
        {
            tmpSong = await _repositorySong.GetByIdAsync(songId);
            if (tmpSong == null)
            {
                throw new NotFoundException("Not found Song", "Song");
            }
            playlist.Songs.Add(tmpSong);
            await UpdateAsync(playlist);
            return playlist;
        }

        throw new BadRequestException("The song already exists in this playlist", "Playlist");
    }

    public async Task<Playlist> DeleteSongFromPlaylist(Guid userId, Guid playlistId, Guid songId)
    {
        var playlist = await BuildQuery().Include(pl => pl.Songs).FirstOrDefaultAsync(pl => pl.Id == playlistId);
        if (playlist == null)
        {
            throw new NotFoundException("Not found Playlist", "Playlist");
        }

        if (playlist.Author.Id != userId)
        {
            throw new InsufficientRightsException("Only author can edit this playlist", "Playlist");
        }

        var tmpSong = playlist.Songs.FirstOrDefault(s => s.Id == songId);
        if (tmpSong != null)
        {
            tmpSong = await _repositorySong.GetByIdAsync(songId);
            playlist.Songs.Remove(tmpSong);
            await UpdateAsync(playlist);
            return playlist;
        }

        throw new BadRequestException("The song doesn't exist in this playlist", "Playlist");
    }

    public async Task<IEnumerable<Playlist>> GetByAuthorAsync(Guid id, PaginationInfo pagination)
    {
        Expression<Func<Playlist, bool>> filter = playlist => playlist.Author.Id == id;
        return await BuildQuery(filter, pagination).ToListAsync();
    }
}