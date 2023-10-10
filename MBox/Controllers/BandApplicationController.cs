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
    public class BandApplicationController : BaseReadController<Application>
    {
        private readonly IBandApplicationService _serviceApp;
        public BandApplicationController(IBandApplicationService service, IHttpResponseHandler response) : base(response, service)
        {
            _serviceApp = service;
        }


        [HttpGet("user/{id}")]
        public async Task<ActionResult<ResponsePresenter>> GetApplicationsByUser(Guid id, [FromQuery] PaginationInfo pagination)
        {
            try
            {
                IEnumerable<Application> items = await _serviceApp.GetByUserAsync(id, pagination);
                return _response.Succes(GetPresenterCollection(items));
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        [HttpGet("status/{id}")]
        public async Task<ActionResult<ResponsePresenter>> GetApplicationsByStatus(Guid id, [FromQuery] PaginationInfo pagination)
        {
            try
            {
                IEnumerable<Application> items = await _serviceApp.GetByStatusAsync(id, pagination);
                return _response.Succes(GetPresenterCollection(items));
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponsePresenter>> CreateAsync(Application application)
        {
            try
            {
                Application createdApplication = await _serviceApp.AddAsync(application);
                return _response.Created(createdApplication);
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponsePresenter>> UpdateAsync(Application application)
        {
            try
            {
                Application updatedapplication = await _serviceApp.UpdateAsync(application);
                return _response.Succes(updatedapplication);
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        [HttpDelete("{id}")]
        public async virtual Task<ActionResult<ResponsePresenter>> DeleteAsync(Guid id)
        {
            try
            {
                await _serviceApp.DeleteAsync(id);
                return _response.Succes();
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }
    }
}
