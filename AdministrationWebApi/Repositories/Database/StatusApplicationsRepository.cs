using AdministrationWebApi.Models.Db;

namespace AdministrationWebApi.Repositories.Database
{
    public class StatusApplicationsRepository:BaseRepository<StatusApplications>
    {
        public StatusApplicationsRepository(AppDb dbContext):base(dbContext) { }
    }
}
