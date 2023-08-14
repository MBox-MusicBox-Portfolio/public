using AdministrationWebApi.Models.Db;

namespace AdministrationWebApi.Repositories.Database
{
    public class MemberBandRepository:BaseRepository<MemberBand>
    {
        public MemberBandRepository(AppDb dbContext):base(dbContext) { }
    }
}
