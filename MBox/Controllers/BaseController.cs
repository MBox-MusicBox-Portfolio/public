using MBox.Models.Pagination;
using MBox.Services.Db.Interfaces;
using MBox.Services.Responce.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MBox.Controllers
{
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        protected readonly IHttpResponseHandler _response;
        protected readonly IBaseService<T> _service;

        public BaseController(IHttpResponseHandler response, IBaseService<T> service)
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
                return _response.Succes(GetPresentCollection(items));
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
                return Ok(GetPresent(item));
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponsePresenter>> CreateAsync(T entity)
        {
            try
            {
                T createdEntity = await _service.AddAsync(entity);
                return _response.Created(GetPresent(createdEntity));
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponsePresenter>> UpdateAsync(T entity)
        {
            try
            {
                await _service.UpdateAsync(entity);
                return _response.NoContent();
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponsePresenter>> DeleteAsync(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return _response.NoContent();
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }
        protected virtual object GetPresent(T entity) => entity;
        protected virtual IEnumerable<object> GetPresentCollection(IEnumerable<T> entity)
        {
            return entity.Select(obj => GetPresent(obj));
        }
    }
}
