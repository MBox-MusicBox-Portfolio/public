using MBox.Models.Db;
using MBox.Models.Exceptions;
using MBox.Models.Pagination;
using MBox.Repositories.Interfaces;
using MBox.Services.Db.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MBox.Services.Db
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<Band> _repositoryBand;
        private readonly IRepository<Playlist> _repositoryPlaylist;
        private readonly IRepository<LikedPlaylist> _repositoryLikedPlaylist;

        public UserService(IRepository<User> repository,
            IRepository<Band> repositoryBand,
            IRepository<Playlist> repositoryPlaylist,
            IRepository<LikedPlaylist> repositoryLikedPlaylist,
            IConfiguration configuration) : base(repository)
        {
            _configuration = configuration;
            _repositoryBand = repositoryBand;
            _repositoryPlaylist = repositoryPlaylist;
            _repositoryLikedPlaylist = repositoryLikedPlaylist;
        }

        public async Task<Playlist> GetUserLibrary(Guid userId)
        {
            var playlist = await _repositoryPlaylist.BuildQuery().FirstOrDefaultAsync(pl =>
            pl.Author.Id == userId &&
            pl.IsPublic == false);
            if (playlist != null)
            {
                return playlist;
            }
            throw new NotFoundException("Not found Playlist", "Playlist");
        }

        public async Task<IEnumerable<Playlist>> GetUserPlaylists(Guid userId, PaginationInfo pagination)
        {
            var playlists = await _repositoryPlaylist.
                BuildQuery()
                .Where(pl => pl.Author.Id == userId)
                .Skip((pagination.PageIndex - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();
            return playlists;
        }

        public async Task<IEnumerable<Playlist>> GetLikedPlaylists(Guid userId, PaginationInfo pagination)
        {
            Expression<Func<User, bool>> filter = user => user.Id == userId;
            var playlists = await _repositoryLikedPlaylist
                .BuildQuery()
                .Where(lPl => lPl.User.Id == userId)
                .Select(lPl => lPl.Playlist)
                .Skip((pagination.PageIndex - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();
            return playlists;
        }

        public async Task<IEnumerable<Band>> GetFollowedBands(Guid userId, PaginationInfo pagination)
        {
            Expression<Func<User, bool>> filter = user => user.Id == userId;
            var user = await BuildQuery(filter, pagination).Include(u => u.FollowingsBands).FirstOrDefaultAsync();
            var bands = user.FollowingsBands.ToList();
            return bands;
        }
    }
}
