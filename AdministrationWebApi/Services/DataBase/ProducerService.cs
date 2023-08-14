using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Models.RequestModels;
using AdministrationWebApi.Repositories.Database.Interfaces;
using AdministrationWebApi.Services.DataBase.Interfaces;
using AdministrationWebApi.Services.ResponseHelper.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdministrationWebApi.Services.DataBase
{
    public class ProducerService : BaseService<Producer>, IByOneMethod<Producer>
    {
        public ProducerService(IEntityRepository<Producer> service) : base(service) { }
        public async Task<IEnumerable<Producer>> GetByOneParams(Guid id, PaginationInfo pagination)
        {
            Expression<Func<Producer, bool>> filter = producer => producer.Id == id;
            return await BuildQuery(filter, pagination).Include(producer=>producer.Bands).ToListAsync();
        }
    }
}
