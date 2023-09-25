using MBox.Models.Db;

namespace MBox.Repositories.MYSQL;

public class NewsRepository : BaseRepository<News>
{
    public NewsRepository(AppDbContext context) : base(context) { }
}