using MBox.Models.Db;
using MBox.Repositories.Interfaces;
using MBox.Services.Db.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MBox.Services.Db
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<Band> _repositoryBand;

        public UserService(IRepository<Song> repository, IRepository<Band> repositoryBand, IConfiguration configuration) : base(repository)
        {
            _configuration = configuration;
            _repositoryBand = repositoryBand;
        }
    }
}
