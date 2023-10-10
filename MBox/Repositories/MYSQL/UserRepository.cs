using MBox.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace MBox.Repositories.MYSQL;

public class UserRepository : BaseRepository<User>
{
    public UserRepository(AppDbContext context) : base(context) { }

    public IQueryable<User> BuildQuery(Guid id)
    {
        return _context.Users
            .Where(u => u.Id == id)
            .Include(user => user.FollowingsBands);
    }
}