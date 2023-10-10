using MBox.Models.Db;
using MBox.Models.Pagination;
using MBox.Models.Presenters;
using MBox.Services.Db.Interfaces;
using MBox.Services.Responce.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MBox.Controllers
{
    [Route("api/public/albums")]
    [ApiController]
    public class AlbumController : BaseReadController<Album> 
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService, IHttpResponseHandler response): base(response, albumService)
        {
            _albumService = albumService;
        }

        [HttpGet("{id}/band/{pagination}")]
        public async Task<ActionResult<ResponsePresenter>> GetAlbumByBand(Guid id, [FromQuery] PaginationInfo pagination)
        {
            try
            {
                IEnumerable<Album> items = await _albumService.GetByBandAsync(id, pagination);
                return _response.Succes(GetPresenterCollection(items));
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }
    }
}
