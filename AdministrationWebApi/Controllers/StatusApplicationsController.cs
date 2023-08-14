using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Services.DataBase.Interfaces;
using AdministrationWebApi.Services.ResponseHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationWebApi.Controllers
{
    [ApiController]
    [Route("api/admin/applications/status")]
    public class StatusApplicationsController : BaseAppController<StatusApplications>
    {
        public StatusApplicationsController(IResponseHelper response, IBaseService<StatusApplications> service):base(response, service) { }        
    }
}
