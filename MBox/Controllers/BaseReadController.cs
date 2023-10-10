using MBox.Models.Pagination;
using MBox.Models.Presenters;
using MBox.Services.Db.Interfaces;
using MBox.Services.Responce.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MBox.Controllers
{
    [ApiController]
    public class BaseReadController<T> : ControllerBase
    {
        protected readonly IHttpResponseHandler _response;
        protected readonly IBaseService<T> _service;

        public BaseReadController(IHttpResponseHandler response, IBaseService<T> service)
        {
            _response = response;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ResponsePresenter>> GetAllAsync([FromQuery] PaginationInfo pagination)
        {
            try
            {
                IEnumerable<T> items = await _service.GetAllAsync(pagination);
                return _response.Succes(GetPresenterCollection(items));
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponsePresenter>> GetByIdAsync(Guid id)
        {
            try
            {
                T item = await _service.GetByIdAsync(id);
                if (item == null)
                {
                    return NotFound();
                }
                return Ok(GetPresenter(item));
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        protected virtual object GetPresenter(T entity) => entity;
        protected virtual IEnumerable<object> GetPresenterCollection(IEnumerable<T> entity)
        {
            return entity.Select(obj => GetPresenter(obj));
        }
    }
}
