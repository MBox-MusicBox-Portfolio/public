using AdministrationWebApi.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace AdministrationWebApi.Repositories.Database
{
    public class PlaylistRepository:BaseRepository<Playlist>
    {
        public PlaylistRepository(AppDb dbContext):base(dbContext) { }
        public override IQueryable<Playlist> BuildQuery()
        {
            return _context.Playlists.Include(u => u.Songs);
        }
    }
}
