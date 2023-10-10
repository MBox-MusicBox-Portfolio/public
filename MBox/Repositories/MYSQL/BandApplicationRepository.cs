using MBox.Models.Db;
using MBox.Models.Pagination;
using MBox.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MBox.Repositories.MYSQL;

public class BandApplicationRepository : BaseRepository<Application>
{
    public BandApplicationRepository(AppDbContext context) : base(context) { }

    public override IQueryable<Application> BuildQuery()
    {
        return _context.Applications
            .Include(app => app.Producer)
            .Include(app => app.Status);
    }
}