using MBox.Models.Db;
using MBox.Models.Pagination;
using MBox.Models.Presenters;
using Microsoft.AspNetCore.Mvc;

namespace MBox.Repositories.Interfaces;

public interface IRepository<T>
{
    public Task<IEnumerable<T>> GetAllAsync(PaginationInfo pagination);
    public Task<T> GetByIdAsync(Guid id);
    public Task<T> AddAsync(T entity);
    public Task<T> UpdateAsync(T entity);
    public Task<bool> DeleteAsync(Guid id);
    public IQueryable<T> BuildQuery();
}