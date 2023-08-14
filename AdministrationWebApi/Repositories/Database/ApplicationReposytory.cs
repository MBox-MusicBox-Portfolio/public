using AdministrationWebApi.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace AdministrationWebApi.Repositories.Database
{
    public class ApplicationReposytory : BaseRepository<Applications>
    {

        public ApplicationReposytory(AppDb context) : base(context)
        {
        }
        public override IQueryable<Applications> BuildQuery()
        {
            return _context.Applications
                .Include(app => app.Producer)
                .Include(app => app.Admin)
                .Include(app => app.Status);
        }
    }
}
