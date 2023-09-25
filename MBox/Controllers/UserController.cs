using MBox.Models.Db;
using MBox.Services.Db.Interfaces;
using MBox.Services.Responce.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MBox.Controllers
{
    [ApiController]
    [Route("api/public/users")]
    public class UserController : BaseController<User>
    {
        public UserController(IHttpResponseHandler response, IBaseService<User> service) : base(response, service)
        {
        }
    }
}
