using MBox.Models.Db;
using MBox.Models.Pagination;
using MBox.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MBox.Repositories.MYSQL;

public class BandApplicationRepository : BaseRepository<BandApplication>
{
    public BandApplicationRepository(AppDbContext context) : base(context) { }

    public override IQueryable<BandApplication> BuildQuery()
    {
        return _context.BandApplications
            .Include(app => app.Producer)
            .Include(app => app.Status);
    }
}