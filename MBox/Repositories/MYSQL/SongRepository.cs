using MBox.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace MBox.Repositories.MYSQL;

public class SongRepository : BaseRepository<Song>
{
    public SongRepository(AppDbContext context) : base(context) { }

    public override IQueryable<Song> BuildQuery()
    {
        return _context.Songs
            .Include(song => song.Author);
    }
}