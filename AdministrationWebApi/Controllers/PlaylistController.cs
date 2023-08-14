using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Models.Presenter;
using AdministrationWebApi.Models.RequestModels;
using AdministrationWebApi.Services.DataBase.Interfaces;
using AdministrationWebApi.Services.ResponseHelper.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationWebApi.Controllers
{
    [Route("api/playlist")]
    [ApiController]
    public class PlaylistController : BaseAppController<Playlist>
    {
        private readonly IByOneMethod<Playlist> _servicePlaylist;
        public PlaylistController(IResponseHelper response, IByOneMethod<Playlist> service) : base(response, service)
        {
            _servicePlaylist = service;
        }

        [HttpGet("{id}/user/pagination")]
        public async Task<ActionResult<ResponsePresenter>> GetPlaylistByUser(Guid id, [FromQuery] PaginationInfo pagination)
        {
            try
            {
                IEnumerable<Playlist> items = await _servicePlaylist.GetByOneParams(id, pagination);
                return _response.Ok(GetPresentCollection(items));
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }
    }
}
