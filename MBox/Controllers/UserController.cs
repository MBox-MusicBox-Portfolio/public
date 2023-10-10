using MBox.Models.Db;
using MBox.Models.Pagination;
using MBox.Models.Presenters;
using MBox.Services.Db.Interfaces;
using MBox.Services.Responce.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;

namespace MBox.Controllers
{
    [ApiController]
    [Route("api/public/users")]
    public class UserController : BaseReadController<User>
    {
        private readonly IUserService _serviceUser;
        public UserController(IHttpResponseHandler response, IUserService service) : base(response, service)
        {
            _serviceUser = service;
        }

        protected override object GetPresenter(User entity)
        {
            return new UserPresenter(entity);
        }

        [HttpGet("{id}/library/user")]
        public async Task<ActionResult<ResponsePresenter>> GetUserLibrary(Guid id)
        {
            try
            {
                Playlist library = await _serviceUser.GetUserLibrary(id);
                return _response.Succes(library);
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        [HttpGet("{id}/user/playlists")]
        public async Task<ActionResult<ResponsePresenter>> GetUserPlaylists(Guid id, [FromQuery] PaginationInfo pagination)
        {
            try
            {
                IEnumerable<Playlist> library = await _serviceUser.GetUserPlaylists(id, pagination);
                return _response.Succes(library);
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        [HttpGet("{id}/user/likedplaylists")]
        public async Task<ActionResult<ResponsePresenter>> GetLikedPlaylists(Guid id, [FromQuery] PaginationInfo pagination)
        {
            try
            {
                IEnumerable<Playlist> likedPlaylists = await _serviceUser.GetLikedPlaylists(id, pagination);
                return _response.Succes(likedPlaylists);
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        [HttpGet("{id}/user/bands")]
        public async Task<ActionResult<ResponsePresenter>> GetFollowedBands(Guid id, [FromQuery] PaginationInfo pagination)
        {
            try
            {
                var bands = await _serviceUser.GetFollowedBands(id, pagination);
                IEnumerable<BandPresenter> bandPresenters = bands.Select(band => new BandPresenter(band));
                return _response.Succes(bandPresenters);
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }
    }
}