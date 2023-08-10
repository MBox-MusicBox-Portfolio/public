using MBox.Models.Pagination;
using MBox.Services.Db.Interfaces;
using System.Linq.Expressions;
using MBox.Repositories.Interfaces;

namespace MBox.Services.Db;

public class BaseService<TEntity> : IBaseService<TEntity>
{
    protected IRepository<TEntity> _repository;
    public BaseService(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        return await _repository.AddAsync(entity);
    }

    public IQueryable<TEntity> BuildQuery()
    {
        return _repository.BuildQuery();
    }
    public IQueryable<TEntity> BuildQuery(Expression<Func<TEntity, bool>> filter, PaginationInfo pagination)
    {
        var applications = BuildQuery()
            .Where(filter)
            .Skip((pagination.PageIndex - 1) * pagination.PageSize)
            .Take(pagination.PageSize);

        return applications;
    }
    public IQueryable<TEntity> BuildQuery(Expression<Func<TEntity, bool>> filter)
    {
        var applications = BuildQuery()
            .Where(filter);

        return applications;
    }

    public virtual async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(PaginationInfo pagination)
    {
        return await _repository.GetAllAsync(pagination);
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        return await _repository.UpdateAsync(entity);
    }
}