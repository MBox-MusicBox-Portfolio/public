using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Models.Presenter;
using AdministrationWebApi.Services.DataBase.Interfaces;
using AdministrationWebApi.Services.ResponseHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace AdministrationWebApi.Controllers
{
    [ApiController]
    [Route("api/admin/users")]
    public class UserController : BaseAppController<User>
    {
        private readonly IUserService _serviceUser;       
        public UserController(IUserService service, IResponseHelper response):base(response,service) 
        {
            _serviceUser = service;           
        }

        // POST: api/admin/users/{id}/changerole
        [HttpPost("{id}/changerole")]
        public async Task<ActionResult<ResponsePresenter>> ChangeUserRole(Guid id, Role newRole) {
            try
            {
                bool isResult = await _serviceUser.ChangeUserRole(id, newRole);
                return _response.Ok(isResult);
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        //protected override object GetPresent(User entity) => new UserPresenter(entity);
    }
}
