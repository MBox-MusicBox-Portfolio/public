using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Models.Presenter;
using AdministrationWebApi.Models.RequestModels;
using AdministrationWebApi.Services.DataBase.Interfaces;
using AdministrationWebApi.Services.ResponseHelper.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationWebApi.Controllers
{
    [Route("api/admin/producer")]
    [ApiController]
    public class ProducerController : BaseAppController<Producer>
    {
        private readonly IByOneMethod<Producer> _serviceProducer;
        public ProducerController(IResponseHelper response, IByOneMethod<Producer> service):base(response, service) {
            _serviceProducer = service;
        }

        [HttpGet("{id}/band/pagination")]
        public async Task<ActionResult<ResponsePresenter>> GetByBand(Guid id, [FromQuery] PaginationInfo pagination)
        {
            try
            {
                IEnumerable<Producer> items = await _serviceProducer.GetByOneParams(id, pagination);               
                return _response.Ok((items.First().Bands));
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }
    }
}
