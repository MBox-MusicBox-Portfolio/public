using MBox.Models.Db;
using MBox.Services.Db.Interfaces;
using MBox.Services.Responce.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MBox.Controllers;

[ApiController]
[Route("api/public/songs")]
public class SongController : BaseController<Song>
{
    private readonly ISongService _serviceSong;
    public SongController(ISongService service, IHttpResponseHandler response) : base(response, service)
    {
        _serviceSong = service;
    }

    [HttpPost("{id}/song/authors")]
    public async Task<ActionResult<ResponsePresenter>> DeleteSongFromPlaylist(Guid song)
    {
        try
        {
            var items = await _serviceSong.GetBandBySong(song);
            return _response.Succes(items);
        }
        catch (Exception ex)
        {
            return _response.HandleError(ex);
        }
    }
}