using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Models.Presenter;
using AdministrationWebApi.Models.RequestModels;
using AdministrationWebApi.Services.DataBase.Interfaces;
using AdministrationWebApi.Services.ResponseHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationWebApi.Controllers
{
    [Route("api/admin/album")]
    [ApiController]
    public class AlbumController : BaseAppController<Album>
    {
        private readonly IByOneMethod<Album> _serviceAlbum;
        public AlbumController(IResponseHelper response, IByOneMethod<Album> servise) : base(response, servise)
        {
            _serviceAlbum = servise;
        }

        [HttpGet("{id}/band/pagination")]
        public async Task<ActionResult<ResponsePresenter>> GetAlbumByBand(Guid id, [FromQuery] PaginationInfo pagination)
        {
            try
            {
                IEnumerable<Album> items = await _serviceAlbum.GetByOneParams(id, pagination);
                return _response.Ok(GetPresentCollection(items));
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }
    }
}
