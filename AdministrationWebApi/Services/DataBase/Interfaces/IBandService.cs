using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Models.Presenter;
using AdministrationWebApi.Services.DataBase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationWebApi.Repositories.DataBase.Interfaces
{
    public interface IBandService: IBaseService<Band>
    { 
        Task<Band> AddMemberToBandAsync(Guid id, Guid memberId);
        Task<bool> BlockBandAsync(Guid id);
        Task<bool> UnblockAsync(Guid id);
    }
}
