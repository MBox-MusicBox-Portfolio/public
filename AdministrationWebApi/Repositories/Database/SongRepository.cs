using AdministrationWebApi.Models.Db;

namespace AdministrationWebApi.Repositories.Database
{
    public class SongRepository:BaseRepository<Song>
    {
        public SongRepository(AppDb dbContext):base(dbContext) { }
    }
}
