using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Models.Presenter;
using AdministrationWebApi.Models.RequestModels;
using AdministrationWebApi.Repositories.DataBase.Interfaces;
using AdministrationWebApi.Services.ResponseHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationWebApi.Controllers
{
    [ApiController]
    [Route("api/admin/news")]
    public class NewsController : BaseAppController<News>
    {
        private readonly INewsService _serviceNews;

        public NewsController(INewsService service, IResponseHelper response) : base(response, service)
        {
            _serviceNews = service;
        }

        // GET: api/news/search/{authorId}/author
        [HttpGet("search/{authorId}/author")]
        public async Task<ActionResult<ResponsePresenter>> GetNewsByAuthor(Guid authorId, [FromQuery] PaginationInfo pagination)
        {

            try
            {
                IEnumerable<News> items = await _serviceNews.GetNewsByAuthor(authorId, pagination);
                return _response.Ok(items);
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }
        // GET: api/news/search/{bandId}/band
        [HttpGet("search/{bandId}/band")]
        public async Task<ActionResult<ResponsePresenter>> GetNewsByBand(Guid bandId, [FromQuery] PaginationInfo pagination)
        {

            try
            {
                IEnumerable<News> items = await _serviceNews.GetNewsByBand(bandId, pagination);
                return _response.Ok(GetPresentCollection(items));
            }
            catch (Exception ex)
            {
                return _response.HandleError(ex);
            }
        }

    }

}
