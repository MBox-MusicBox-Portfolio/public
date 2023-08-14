using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Models.RequestModels;
using AdministrationWebApi.Repositories.Database.Interfaces;
using AdministrationWebApi.Services.DataBase.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdministrationWebApi.Services.DataBase
{
    public class MemberBandService : BaseService<MemberBand>, IByOneMethod<MemberBand>
    {
        public MemberBandService(IEntityRepository<MemberBand> repository) : base(repository) { }
        public async Task<IEnumerable<MemberBand>> GetByOneParams(Guid id, PaginationInfo pagination)
        {
            Expression<Func<MemberBand, bool>> filter = member => member.Id == id;
            return await BuildQuery(filter, pagination).Include(member => member.Bands).ToListAsync();
        }
    }
}
