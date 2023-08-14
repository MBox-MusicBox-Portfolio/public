using AdministrationWebApi.Models.Db;
using AdministrationWebApi.Models.Exceptions;
using AdministrationWebApi.Models.Presenter;
using AdministrationWebApi.Repositories.Database.Interfaces;
using AdministrationWebApi.Services.ActionsMailer;
using AdministrationWebApi.Services.DataBase.Interfaces;
using AdministrationWebApi.Services.ResponseHelper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdministrationWebApi.Services.DataBase
{
    public class SongService : BaseService<Song>,ISongService
    {
       
        private readonly IActionMailer _mailer;
        private readonly IConfiguration _configuration;
        private readonly IResponseHelper _response;
        public SongService(IEntityRepository<Song> repository, IActionMailer mailer, IConfiguration configuration, IResponseHelper response):base(repository)
        {
            _mailer = mailer;
            _configuration = configuration;
           
        }

        public async Task<bool> BlockSong(Guid id)
        {
            var song = await GetByIdAsync(id);

            if (song == null)
            {
                throw new NotFoundException(" Not found Song", "Song");
            }
            song.IsBlock = true;
            await UpdateAsync(song);
            _ = _mailer.SongAction(song, _configuration["TemplatePages:BLOCK_SONG"]);
            return true;
        }

        public async Task<bool> UnBlockSong(Guid id)
        {
            var song = await GetByIdAsync(id);

            if (song == null)
            {
                throw new NotFoundException(" Not found Song", "Song");
            }
            song.IsBlock = false;
            await UpdateAsync(song);
            _ = _mailer.SongAction(song, _configuration["TemplatePages:UNBLOCK_SONG"]);
            return true;
        }

    }
}
