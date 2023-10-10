using MBox.Models.Db;
using MBox.Repositories.Interfaces;
using MBox.Services.Db.Interfaces;

namespace MBox.Services.Db
{
    public class NewsService : BaseService<News>, INewsService
    {
        public NewsService(IRepository<News> repository) : base(repository) { }
    }
}
