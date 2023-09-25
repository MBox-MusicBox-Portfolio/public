using MBox.Models.Db;

namespace MBox.Repositories.MYSQL;

public class BandRepository : BaseRepository<Band>
{
    public BandRepository(AppDbContext context) : base(context) { }
}