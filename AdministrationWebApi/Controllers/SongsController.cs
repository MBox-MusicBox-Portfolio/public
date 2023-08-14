using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Models.Presenter;
using AdministrationWebApi.Models.RequestModels;
using AdministrationWebApi.Services.DataBase.Interfaces;
using AdministrationWebApi.Services.ResponseHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationWebApi.Controllers
{
    [ApiController]
    [Route("api/admin/songs")]
    public class SongsController : BaseAppController<Song>
    {
        private readonly ISongService _serviceSong;       

        public SongsController(ISongService service,IResponseHelper response):base(response,service) 
        {
            _serviceSong = service;          
        }
        
        // PUT: api/songs/{id}/block
        [HttpPut("{id}/block")]
        public async Task<ActionResult<ResponsePresenter>> BlockSong(Guid id)
        {
            try
            {
                var isResult = await _serviceSong.BlockSong(id);
                return _response.Ok(isResult);
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

        // PUT: api/songs/{id}/block
        [HttpPut("{id}/unblock")]
        public async Task<ActionResult<ResponsePresenter>> UnBlockSong(Guid id)
        {
            try
            {
                var isResult = await _serviceSong.UnBlockSong(id);
                return _response.Ok(isResult);
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

    }

}
