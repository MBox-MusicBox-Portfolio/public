using MBox.Models.Db;
using MBox.Models.Pagination;
using MBox.Services.Db.Interfaces;
using MBox.Services.Responce.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MBox.Controllers
{
    [ApiController]
    [Route("api/public/playlists")]
    public class PlaylistController : BaseController<Playlist>
    {
        private readonly IPlaylistService _servicePlaylist;
        public PlaylistController(IPlaylistService service, IHttpResponseHandler response) : base(response, service)
        {
            _servicePlaylist = service;
        }

        // GET: api/public/playlists/{id}/author
        [HttpGet("{id}/author")]
        public async Task<ActionResult<ResponsePresenter>> GetByAuthorAsync(Guid id, [FromQuery] PaginationInfo pagination)
        {
            try
            {
                IEnumerable<Playlist> items = await _servicePlaylist.GetByAuthorAsync(id, pagination);
                return _response.Succes(GetPresentCollection(items));
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        [HttpPost("{id}/user/{id}/song")]
        public async Task<ActionResult<ResponsePresenter>> AddSongToPlaylist(Guid id, [FromBody] Guid user, [FromBody] Guid song)
        {
            try
            {
                var item = await _servicePlaylist.AddSongToPlaylist(user, id, song);
                return _response.Succes(item);
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        [HttpPost("{id}/user/{id}/song")]
        public async Task<ActionResult<ResponsePresenter>> DeleteSongFromPlaylist(Guid id, [FromBody] Guid user, [FromBody] Guid song)
        {
            try
            {
                var item = await _servicePlaylist.DeleteSongFromPlaylist(user, id, song);
                return _response.Succes(item);
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }
    }
}
