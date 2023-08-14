using AdministrationWebApi.Models.Db;

namespace AdministrationWebApi.Repositories.Database
{
    public class ProducerRepository:BaseRepository<Producer>
    {
        public ProducerRepository(AppDb  dbContext) : base(dbContext) { }
    }
}
