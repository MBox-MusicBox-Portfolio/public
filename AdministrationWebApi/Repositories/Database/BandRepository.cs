using AdministrationWebApi.Models.Db;

namespace AdministrationWebApi.Repositories.Database
{
    public class BandRepository:BaseRepository<Band>
    {
        public BandRepository(AppDb dbContexto) : base(dbContexto) { }
    }
}
