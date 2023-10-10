using MBox.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace MBox.Repositories.MYSQL
{
    public class LikedPlaylistsRepository : BaseRepository<LikedPlaylist>
    {
        public LikedPlaylistsRepository(AppDbContext context) : base(context) { }
    }
}
