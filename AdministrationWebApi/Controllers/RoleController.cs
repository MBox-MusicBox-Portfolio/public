using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Services.DataBase.Interfaces;
using AdministrationWebApi.Services.ResponseHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationWebApi.Controllers
{
    [Route("api/admin/role")]
    [ApiController]
    public class RoleController : BaseAppController<Role>
    {
        public RoleController(IResponseHelper response, IBaseService<Role> service) : base(response, service) { }
    }
}
