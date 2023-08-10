using MBox.Models.Db;
using MBox.Models.Pagination;
using MBox.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MBox.Repositories.MYSQL;

public class BandApplicationRepository : BaseRepository<BandApplication>
{
    private readonly ILogger? _logger;

    public BandApplicationRepository(AppDbContext context) : base(context) { }

    /*
     * Gets all aplications that userId has
     */
    public async Task<IEnumerable<BandApplication>> GetAllAsync(Guid userId, PaginationInfo pagination)
    {
        var totalEntities = await BuildQuery().ToListAsync();
        var totalCount = totalEntities.Count;

        var totalPages = (int)Math.Ceiling((double)totalCount / pagination.PageSize);

        if (pagination.PageIndex <= 0 || pagination.PageIndex > totalPages)
        {
            return Enumerable.Empty<BandApplication>();
        }

        var entities = await _context.BandApplications
            .Where(application => application.Producer != null && application.Producer.Id == userId)
            .Skip((pagination.PageIndex - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        return entities;
    }
}