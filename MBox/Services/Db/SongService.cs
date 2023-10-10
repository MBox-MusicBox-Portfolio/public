using MBox.Models.Db;
using MBox.Repositories.Interfaces;
using MBox.Services.Db.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MBox.Services.Db;

public class SongService : BaseService<Song>, ISongService
{
    private readonly IConfiguration _configuration;
    private readonly IRepository<Band> _repositoryBand;

    public SongService(IRepository<Song> repository, IRepository<Band> repositoryBand, IConfiguration configuration) : base(repository)
    {
        _configuration = configuration;
        _repositoryBand = repositoryBand;
    }

    public async Task<IEnumerable<Song>> GetSongByTerm(string term)
    {
        Expression<Func<Song, bool>> filter = song => song.Name.Contains(term);
        List<Song> songs = await BuildQuery(filter).ToListAsync();
        if (songs != null)
        {
            return songs;
        }
        return new List<Song>();
    }

    public async Task<IEnumerable<Band>> GetBandBySong(Guid songId)
    {
        Expression<Func<Song, bool>> filter = song => song.Id == songId;
        var song = await BuildQuery(filter)
            .Include(song => song.Performer)
            .SingleOrDefaultAsync();
        if (song != null)
        {
            return song.Performer.ToList();
        }
        return new List<Band>();
    }
}