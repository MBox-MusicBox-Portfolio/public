using MBox.Models.Db;

namespace MBox.Services.Db.Interfaces;

public interface ISongService : IBaseService<Song>
{
    Task<IEnumerable<Band>> GetBandBySong(Guid songId);
    Task<IEnumerable<Song>> GetSongByTerm(string term);
}