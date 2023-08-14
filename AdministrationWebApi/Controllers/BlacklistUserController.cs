using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Models.Presenter;
using AdministrationWebApi.Models.RequestModels;
using AdministrationWebApi.Repositories.DataBase.Interfaces;
using AdministrationWebApi.Services.DataBase.Interfaces;
using AdministrationWebApi.Services.ResponseHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AdministrationWebApi.Controllers
{
    [Route("api/admin/blacklist")]
    [ApiController]
    public class BlacklistUserController : BaseAppController<BlacklistUser>
    {
        private readonly IBlacklistService _serviceBlacklist;
        
        public BlacklistUserController(IBlacklistService service, IResponseHelper response):base(response,service)
        {
            _serviceBlacklist = service;
        }

        // GET: api/admin/blacklist/{id}/user
        [HttpGet("{id}/user")]
        public async Task<ActionResult<ResponsePresenter>> GetHistoryBlacklistByUser(Guid id, PaginationInfo pagination)
        {
            try
            {
                var items = await _serviceBlacklist.GetByUserAsync(id,pagination);
                return _response.Ok(GetPresentCollection(items));
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        // GET: api/admin/blacklist/{id}/admin
        [HttpGet("{id}/admin")]
        public async Task<ActionResult<ResponsePresenter>> GetHistoryBlacklistByAdmin(Guid id, PaginationInfo pagination)
        {
            try
            {
                var items = await _serviceBlacklist.GetByAdminAsync(id, pagination);
                return _response.Ok(GetPresentCollection(items));
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        //// GET: api/admin/blacklist/{isBlock}/status
        //[HttpGet("{isBlock}/status")]
        //public async Task<ActionResult<ResponsePresenter>> GetBlockedOrUnblockedUsers(bool isBlock)
        //{
        //    try
        //    {
        //        var items = await _service.CommonSample(isBlock);
        //        return _response.Ok(GetBlacklistPresenter(items));
        //    }
        //    catch (Exception ex)
        //    {
        //        return _response.HandleError(ex);
        //    }
        //}

        // PUT: api/admin/blacklist/{id}/block}
        [HttpPut("{id}/block")]
        public async Task<ActionResult<ResponsePresenter>> BlockUser(Guid id, CommonRequst request)
        {
            try
            {
                var item = await _serviceBlacklist.BlockUserAsync(id, request);
                return _response.Ok(item);
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        // PUT: api/admin/blacklist/{id}/unblock}
        [HttpPut("{id}/unblock")]
        public async Task<ActionResult<ResponsePresenter>> UnblockUser(Guid id)
        {
            try
            {
                var item = await _serviceBlacklist.UnblockUserAsync(id);
                return _response.Ok(item);
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }
      

        protected override object GetPresent(BlacklistUser entity) => new BlacklistPresenter(entity);
        
    }
}
