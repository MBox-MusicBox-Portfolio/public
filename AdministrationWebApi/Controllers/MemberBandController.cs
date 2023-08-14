using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Models.Presenter;
using AdministrationWebApi.Models.RequestModels;
using AdministrationWebApi.Services.DataBase.Interfaces;
using AdministrationWebApi.Services.ResponseHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationWebApi.Controllers
{
    [Route("api/admin/memberband")]
    [ApiController]
    public class MemberBandController : BaseAppController<MemberBand>
    {
        private readonly IByOneMethod<MemberBand> _serviceMember;
        public MemberBandController(IResponseHelper response, IByOneMethod<MemberBand> servise) : base(response, servise)
        {
            _serviceMember = servise;
        }

        [HttpGet("{id}/pagination")]
        public async Task<ActionResult<ResponsePresenter>> GetBandsByMember(Guid id, [FromQuery] PaginationInfo pagination)
        {
            try
            {
                IEnumerable<MemberBand> items = await _serviceMember.GetByOneParams(id, pagination);
                var result = items.First().Bands;
                return _response.Ok(result);
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }
    }
}
