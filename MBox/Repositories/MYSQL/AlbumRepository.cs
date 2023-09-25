using MBox.Models.Db;

namespace MBox.Repositories.MYSQL;

public class AlbumRepository : BaseRepository<Album>
{
    public AlbumRepository(AppDbContext  context):base(context){}
}