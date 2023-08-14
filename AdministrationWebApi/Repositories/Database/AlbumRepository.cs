using AdministrationWebApi.Models.Db;

namespace AdministrationWebApi.Repositories.Database
{
    public class AlbumRepository : BaseRepository<Album>
    {
        public AlbumRepository(AppDb dbContext):base(dbContext) { }
    }
}
