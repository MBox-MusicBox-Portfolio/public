using MBox.Models.Db;
using MBox.Models.Pagination;
using MBox.Services.Db.Interfaces;
using MBox.Services.Responce.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MBox.Controllers;

[ApiController]
[Route("api/public/bands")]
public class BandController : BaseController<Band>
{
    private readonly IBandService _serviceBand;
    public BandController(IBandService service, IHttpResponseHandler response) : base(response, service)
    {
        _serviceBand = service;
    }

    [HttpPut("{band}/band/{user}/user")]
    public async Task<ActionResult<ResponsePresenter>> AddSongToPlaylist(Guid user, Guid band)
    {
        try
        {
            var item = await _serviceBand.FollowBand(user, band);
            return _response.Succes(item);
        }
        catch (Exception ex)
        {
            return _response.HandleError(ex);
        }
    }

    [HttpDelete("{band}/band/{user}/user")]
    public async Task<ActionResult<ResponsePresenter>> DeleteSongFromPlaylist(Guid user, Guid band)
    {
        try
        {
            var item = await _serviceBand.UnFollowBand(user, band);
            return _response.Succes(item);
        }
        catch (Exception ex)
        {
            return _response.HandleError(ex);
        }
    }
}