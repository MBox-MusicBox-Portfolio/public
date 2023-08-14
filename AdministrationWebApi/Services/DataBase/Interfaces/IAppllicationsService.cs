using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Models.RequestModels;

namespace AdministrationWebApi.Services.DataBase.Interfaces
{
    public interface IAppllicationsService : IBaseService<Applications>
    {
        Task<IEnumerable<Applications>> GetByUserAsync(Guid id, PaginationInfo pagination);
        Task<IEnumerable<Applications>> GetByAdminAsync(Guid id, PaginationInfo pagination);
        Task<IEnumerable<Applications>> GetByStatusAsync(Guid id, PaginationInfo pagination);
        Task<Applications> ChangeStatusApplicationsAsync(Guid id, CommonRequst entity);
    }
}
