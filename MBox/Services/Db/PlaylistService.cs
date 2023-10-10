using System.Linq.Expressions;
using MBox.Models.Db;
using MBox.Models.Exceptions;
using MBox.Models.Helpers;
using MBox.Models.Pagination;
using MBox.Models.RabbitMq;
using MBox.Repositories.Interfaces;
using MBox.Services.Db.Interfaces;
using MBox.Services.RabbitMQ;
using Microsoft.EntityFrameworkCore;

namespace MBox.Services.Db;

public class PlaylistService : BaseService<Playlist>, IPlaylistService
{
    private readonly IConfiguration _configuration;
    private readonly IRepository<Song> _repositorySong;
    private readonly IRepository<User> _repositoryUser;
    private readonly IRepository<LikedPlaylist> _repositoryLikedPlaylist;
    private readonly RabbitMqService _rabbit;

    public PlaylistService(IConfiguration configuration,
        RabbitMqService rabbit,
        IRepository<Playlist> repositoryPlaylist,
        IRepository<Song> repositorySong,
        IRepository<User> repositoryUser,
        IRepository<LikedPlaylist> repositoryLikedPlaylist)
        : base(repositoryPlaylist)
    {
        _configuration = configuration;
        _repositorySong = repositorySong;
        _repositoryUser = repositoryUser;
        _repositoryLikedPlaylist = repositoryLikedPlaylist;
        _rabbit = rabbit;
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

    public async Task<bool> FollowPlaylist(Guid userId, Guid playlistId)
    {
        var playlist = await BuildQuery().FirstOrDefaultAsync(pl => pl.Id == playlistId);
        if (playlist == null) { throw new BadRequestException("This playlist doesn't exist", "Playlist"); }

        var tmpUser = await _repositoryUser.GetByIdAsync(userId);
        if (tmpUser != null)
        {
            LikedPlaylist likedPlaylist = new LikedPlaylist
            {
                Playlist = playlist,
                User = tmpUser
            };
            var eventObj = new EventMessage()
            {
                From = userId.ToString(),
                Body = new
                {
                    tmpUser.Name
                },
                Template = "follow_playlist",
                To = likedPlaylist.Playlist.Author.Name
            };
            _rabbit.SendMessage(eventObj);
            await _repositoryLikedPlaylist.AddAsync(likedPlaylist);
            return true;
        }

        throw new BadRequestException("This user doesn't exist", "Playlist");
    }

    public async Task<bool> UnfollowPlaylist(Guid userId, Guid playlistId)
    {
        var tmpUser = await _repositoryUser.GetByIdAsync(userId);
        if (tmpUser != null)
        {
            var likedPlaylist = await _repositoryLikedPlaylist.BuildQuery().
                FirstOrDefaultAsync(lkPl =>
                lkPl.Playlist.Id == playlistId &&
                lkPl.User.Id == userId);

            if (likedPlaylist == null) { throw new BadRequestException("This user doesn't follow this playlist", "Playlist"); }

            await _repositoryLikedPlaylist.DeleteAsync(likedPlaylist.Id);
            return true;
        }
        throw new BadRequestException("This user doesn't exist", "Playlist");
    }

    public async Task<Playlist> AddAsync(PlaylistHelper helper)
    {
        if (helper != null)
        {
            var author = await _repositoryUser.GetByIdAsync(helper.AuthorId);
            if (author == null)
            {
                throw new BadRequestException("This user doesn't exist", "Playlist");
            }
            if(helper.Name == "UserLibrary")
            {
                throw new BadRequestException("Playlist can't have this name", "Playlist");
            }
            Playlist playlist = new Playlist
            {
                Id = helper.Id,
                Author = author,
                CreatedAt = helper.CreatedAt,
                Name = helper.Name,
                IsPublic = helper.IsPublic
            };
            var res = await AddAsync(playlist);
            return res;
        }
        throw new BadRequestException("Playlist can't be empty", "Playlist");
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId)
    {
        var playlist = await GetByIdAsync(id);
        if (playlist.Author.Id != userId)
        {
            throw new BadRequestException("This user is not author of this playlist", "Playlist");
        }
        if (playlist != null)
        {
            bool res = await DeleteAsync(playlist.Id);
            return res;
        }
        throw new BadRequestException("This playlist doesn't exist", "Playlist");
    }

    public async Task<Playlist> UpdateAsync(Playlist playlist, Guid userId)
    {
        if (playlist != null)
        {
            if (playlist.Author.Id != userId)
            {
                throw new BadRequestException("This user is not author of this playlist", "Playlist");
            }
            var res = await UpdateAsync(playlist);
            return res;
        }
        throw new BadRequestException("This playlist doesn't exist", "Playlist");
    }
}