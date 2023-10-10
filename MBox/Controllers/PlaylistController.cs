using MBox.Models.Db;
using MBox.Models.Helpers;
using MBox.Models.Pagination;
using MBox.Models.Presenters;
using MBox.Services.Db.Interfaces;
using MBox.Services.Responce.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MBox.Controllers
{
    [ApiController]
    [Route("api/public/playlists")]
    public class PlaylistController : BaseReadController<Playlist>
    {
        private readonly IPlaylistService _servicePlaylist;
        public PlaylistController(IPlaylistService service, IHttpResponseHandler response) : base(response, service)
        {
            _servicePlaylist = service;
        }

        [HttpGet("author/{id}")]
        public async Task<ActionResult<ResponsePresenter>> GetByAuthorAsync(Guid id, [FromQuery] PaginationInfo pagination)
        {
            try
            {
                IEnumerable<Playlist> items = await _servicePlaylist.GetByAuthorAsync(id, pagination);
                return _response.Succes(GetPresenterCollection(items));
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        [HttpPost("{id}/user/{user}/song/{song}")]
        public async Task<ActionResult<ResponsePresenter>> AddSongToPlaylist(Guid id, Guid user, Guid song)
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

        [HttpDelete("{id}/user/{user}/song/{song}")]
        public async Task<ActionResult<ResponsePresenter>> DeleteSongFromPlaylist(Guid id, Guid user, Guid song)
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

        [HttpPost("playlist/{playlist}/user/{user}")]
        public async Task<ActionResult<ResponsePresenter>> FollowPlaylist(Guid playlist, Guid user)
        {
            try
            {
                bool res = await _servicePlaylist.FollowPlaylist(user, playlist);
                return _response.Succes(res);
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        [HttpDelete("playlist/{playlist}/user/{user}")]
        public async Task<ActionResult<ResponsePresenter>> UnfollowPlaylist(Guid playlist, Guid user)
        {
            try
            {
                bool res = await _servicePlaylist.UnfollowPlaylist(user, playlist);
                return _response.Succes(res);
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponsePresenter>> CreateAsync(PlaylistHelper playlistHelp)
        {
            try
            {
                Playlist createdPlaylist = await _servicePlaylist.AddAsync(playlistHelp);
                return _response.Created(GetPresenter(createdPlaylist));
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponsePresenter>> UpdateAsync(Playlist playlist)
        {
            try
            {
                Playlist updatedPlaylist = await _service.UpdateAsync(playlist);
                return _response.Succes(updatedPlaylist);
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
                await _service.DeleteAsync(id);
                return _response.Succes();
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }
    }
}