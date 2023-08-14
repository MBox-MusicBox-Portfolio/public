using AdministrationWebApi.Models.Db;

namespace AdministrationWebApi.Repositories.Database
{
    public class NewsRepository:BaseRepository<News>
    {
        public NewsRepository(AppDb dbContext):base(dbContext) { }
    }
}
