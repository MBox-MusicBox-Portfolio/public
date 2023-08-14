using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Models.Presenter;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationWebApi.Services.DataBase.Interfaces
{
    public interface ISongService:IBaseService<Song>
    {
        public Task<bool> BlockSong(Guid id);
        public Task<bool> UnBlockSong(Guid id);
    }
}
