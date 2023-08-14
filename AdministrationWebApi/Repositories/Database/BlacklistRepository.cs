using AdministrationWebApi.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace AdministrationWebApi.Repositories.Database
{
    public class BlacklistRepository : BaseRepository<BlacklistUser>
    {
       
        public BlacklistRepository(AppDb dbContext):base(dbContext) { }

        public override IQueryable<BlacklistUser> BuildQuery()
        {
            return _context.Blacklists
                .Include(bl => bl.User)
                .Include(bl => bl.Admin);
        }
    }
}
