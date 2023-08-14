using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Models.RequestModels;
using AdministrationWebApi.Repositories.Database.Interfaces;
using AdministrationWebApi.Services.DataBase.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdministrationWebApi.Services.DataBase
{
    public class AlbumService:BaseService<Album>, IByOneMethod<Album>
    {        
        public AlbumService(IEntityRepository<Album> repository) : base(repository) {}

        public async Task<IEnumerable<Album>> GetByOneParams(Guid id, PaginationInfo pagination)
        {
            Expression<Func<Album, bool>> filter = album => album.Band.Id == id;
            return await BuildQuery(filter, pagination).ToListAsync();
        }
    }
}
