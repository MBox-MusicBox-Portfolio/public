using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Models.Exceptions;
using AdministrationWebApi.Repositories.Database.Interfaces;
using AdministrationWebApi.Services.ActionsMailer;
using AdministrationWebApi.Services.DataBase;
using AdministrationWebApi.Services.DataBase.Interfaces;

namespace AdministrationWebApi.Repositories.DataBase
{
    public class UserService : BaseService<User>, IUserService
    {

        private readonly IActionMailer _mailer;
        private readonly IConfiguration _configuration;
        private readonly IEntityRepository<Role> _repositoryRole;
        public UserService(IEntityRepository<User> repository,
            IActionMailer mailer,
            IConfiguration configuration,
            IEntityRepository<Role> repositoryRole)
            : base(repository)
        {

            _mailer = mailer;
            _configuration = configuration;
            _repositoryRole = repositoryRole;
        }

        public async Task<bool> ChangeUserRole(Guid id, Role newRole)
        {
            var user = await GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("Not found User", "User");
            }
            var role = await _repositoryRole.GetByIdAsync(newRole.Id);
            if (role == null)
            {
                throw new NotFoundException("Not found Role for User", "User");
            }
            user.Role = role;    
            await UpdateAsync(user);
            await _mailer.UserAction(user, _configuration["TemplatePages:USER_CHANGE_ROLE"]);
            return true;
        }
    }

}
