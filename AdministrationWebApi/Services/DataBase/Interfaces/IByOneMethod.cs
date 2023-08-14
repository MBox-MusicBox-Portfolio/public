using AdministrationWebApi.Models.RequestModels;

namespace AdministrationWebApi.Services.DataBase.Interfaces
{
    public interface IByOneMethod<TEntity> : IBaseService<TEntity>
    {
        public Task<IEnumerable<TEntity>> GetByOneParams(Guid id, PaginationInfo pagination);
    }
}
