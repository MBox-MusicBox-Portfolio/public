using MBox.Models.Db;
using MBox.Models.Pagination;

namespace MBox.Services.Db.Interfaces;

public interface IBandApplicationService : IBaseService<BandApplication>
{
    Task<IEnumerable<BandApplication>> GetByUserAsync(Guid id, PaginationInfo pagination);
    Task<IEnumerable<BandApplication>> GetByStatusAsync(Guid id, PaginationInfo pagination);
}