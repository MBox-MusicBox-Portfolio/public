using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Models.Presenter;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationWebApi.Services.DataBase.Interfaces
{
    public interface IUserService: IBaseService<User>
    {
        public Task<bool> ChangeUserRole(Guid id, Role newRole);
    }
}
