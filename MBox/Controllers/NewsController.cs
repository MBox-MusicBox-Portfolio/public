using MBox.Models.Db;
using MBox.Services.Db.Interfaces;
using MBox.Services.Responce.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MBox.Controllers;

[ApiController]
[Route("api/public/news")]
public class NewsController : BaseController<News>
{
    private readonly INewsService _serviceNews;
    public NewsController(INewsService service, IHttpResponseHandler response) : base(response, service)
    {
        _serviceNews = service;
    }
}