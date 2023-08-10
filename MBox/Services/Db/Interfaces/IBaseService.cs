using MBox.Models.Pagination;

namespace MBox.Services.Db.Interfaces;

public interface IBaseService<TEntity>
{
    Task<TEntity> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync(PaginationInfo pagination);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(Guid id);
    IQueryable<TEntity> BuildQuery();
}