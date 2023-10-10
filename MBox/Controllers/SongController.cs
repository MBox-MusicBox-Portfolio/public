using MBox.Models.Db;
using MBox.Models.Presenters;
using MBox.Services.Db.Interfaces;
using MBox.Services.Responce.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MBox.Controllers;

[ApiController]
[Route("api/public/songs")]
public class SongController : BaseReadController<Song>
{
    private readonly ISongService _serviceSong;
    public SongController(ISongService service, IHttpResponseHandler response) : base(response, service)
    {
        _serviceSong = service;
    }

    [HttpGet("search/{term}")]
    public async Task<ActionResult<ResponsePresenter>> SearchSong(string term)
    {
        try
        {
            var songs = await _serviceSong.GetSongByTerm(term);
            if (songs == null) { return _response.NoContent(); }
            return _response.Succes(songs);
        }
        catch (Exception ex)
        {
            return _response.HandleError(ex);
        }
    }
}