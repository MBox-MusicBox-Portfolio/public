using AdministrationWebApi.Models.Db;

namespace AdministrationWebApi.Repositories.Database
{
    public class RoleRepository:BaseRepository<Role>
    {
        public RoleRepository(AppDb dbContext):base(dbContext) { }
    }
}
