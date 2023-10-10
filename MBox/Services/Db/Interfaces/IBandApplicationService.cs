using MBox.Models.Db;
using MBox.Models.Pagination;

namespace MBox.Services.Db.Interfaces;

public interface IBandApplicationService : IBaseService<Application>
{
    Task<IEnumerable<Application>> GetByUserAsync(Guid id, PaginationInfo pagination);
    Task<IEnumerable<Application>> GetByStatusAsync(Guid id, PaginationInfo pagination);
}