using MBox.Models.Db;
using MBox.Models.Pagination;

namespace MBox.Services.Db.Interfaces;

public interface IAlbumService : IBaseService<Album>
{
    Task<IEnumerable<Album>> GetByBandAsync(Guid id, PaginationInfo pagination);
}