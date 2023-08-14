using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Models.Presenter;
using AdministrationWebApi.Models.RequestModels;
using AdministrationWebApi.Services.DataBase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationWebApi.Repositories.DataBase.Interfaces
{
    public interface INewsService: IBaseService<News>
    {
        public Task<IEnumerable<News>> GetNewsByAuthor(Guid authorId, PaginationInfo pagination);
        public Task<IEnumerable<News>> GetNewsByBand(Guid bandId, PaginationInfo pagination);
    }
}
