using MBox.Models.Db;
using MBox.Models.Pagination;
using MBox.Models.Presenters;
using MBox.Services.Db.Interfaces;
using MBox.Services.Responce.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MBox.Controllers;

[ApiController]
[Route("api/public/bands")]
public class BandController : BaseReadController<Band>
{
    private readonly IBandService _serviceBand;
    public BandController(IBandService service, IHttpResponseHandler response) : base(response, service)
    {
        _serviceBand = service;
    }

    [HttpGet("search/{term}")]
    public async Task<ActionResult<ResponsePresenter>> SearchBand(string term)
    {
        try
        {
            var bands = await _serviceBand.GetBandByTerm(term);
            if (bands == null) { return _response.NoContent(); }
            return _response.Succes(bands);
        }
        catch (Exception ex)
        {
            return _response.HandleError(ex);
        }
    }


    [HttpPut("band/{band}/user/{user}")]
    public async Task<ActionResult<ResponsePresenter>> FollowBand(Guid user, Guid band)
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

    [HttpDelete("band/{band}/user/{user}")]
    public async Task<ActionResult<ResponsePresenter>> UnfollowBand(Guid user, Guid band)
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

    protected override object GetPresenter(Band entity)
    {
        return new BandPresenter(entity);
    }
}