using System.Linq.Expressions;
using MBox.Models.Db;
using MBox.Models.Pagination;
using MBox.Repositories.Interfaces;
using MBox.Services.Db.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MBox.Services.Db;

public class AlbumService:BaseService<Album>, IAlbumService
{
    public AlbumService(IRepository<Album> repository) : base(repository){}

    public async Task<IEnumerable<Album>> GetByBandAsync(Guid bandId, PaginationInfo pagination)
    {
        Expression<Func<Album, bool>> filter = album => album.Author.Id == bandId;
        return await BuildQuery(filter, pagination).ToListAsync();
    }
}