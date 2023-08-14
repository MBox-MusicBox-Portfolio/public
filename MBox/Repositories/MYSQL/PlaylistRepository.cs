using MBox.Models.Db;
using MBox.Models.Presenters;
using MBox.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MBox.Repositories.MYSQL;

public class PlaylistRepository : BaseRepository<Playlist>
{
    private readonly ILogger? _logger;

    public PlaylistRepository(AppDbContext context) : base(context) { }

    public override IQueryable<Playlist> BuildQuery()
    {
        return _context.Playlists
            .Include(playlist => playlist.Author);
    }
}