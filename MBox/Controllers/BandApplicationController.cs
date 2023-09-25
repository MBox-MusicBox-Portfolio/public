using MBox.Models.Db;
using MBox.Models.Pagination;
using MBox.Models.Presenters;
using MBox.Services.Db.Interfaces;
using MBox.Services.Responce.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MBox.Controllers
{
    [ApiController]
    [Route("api/public/applications")]
    public class BandApplicationController : BaseController<BandApplication>
    {
        private readonly IBandApplicationService _serviceApp;
        public BandApplicationController(IBandApplicationService service, IHttpResponseHandler response) : base(response, service)
        {
            _serviceApp = service;
        }

        // GET: api/public/applications/{id}/user
        [HttpGet("{id}/user/{pagination}")]
        public async Task<ActionResult<ResponsePresenter>> GetApplicationsByUser(Guid id, [FromQuery] PaginationInfo pagination)
        {
            try
            {
                IEnumerable<BandApplication> items = await _serviceApp.GetByUserAsync(id, pagination);
                return _response.Succes(GetPresentCollection(items));
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        // GET: api/public/applications/{id}/status
        [HttpGet("{id}/status/{pagination}")]
        public async Task<ActionResult<ResponsePresenter>> GetApplicationsByStatus(Guid id, [FromQuery] PaginationInfo pagination)
        {
            try
            {
                IEnumerable<BandApplication> items = await _serviceApp.GetByStatusAsync(id, pagination);
                return _response.Succes(GetPresentCollection(items));
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }
    }
}
