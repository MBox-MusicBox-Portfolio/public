using MBox.Models.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MBox.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylists()
        {
            var playlists = await _context.Playlists.ToListAsync();
            return Ok(playlists);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Playlist>> GetPlaylist(Guid id)
        {
            var playlist = await _context.Playlists.FindAsync(id);

            if (playlist == null)
            {
                return NotFound();
            }

            return Ok(playlist);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User newUser)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return View();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, User newUser)
        {
            try
            {
                var oldUser = _context.Users.FirstOrDefault(x => x.Id == id);
                if (oldUser == null) return NotFound();

                oldUser.Email = newUser.Email;
                //Other properties . . .


                _context.Users.Update(oldUser);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return View();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var playlist = _context.Playlists.Find(id);
                if (playlist == null) return NotFound();

                _context.Playlists.Remove(playlist);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return View();
            }
        }

        public async Task<IActionResult> AddToPlaylist(Guid playlistId, Guid songId)
        {
            try
            {
                var song = _context.Songs.Find(songId);
                var playlist = _context.Playlists.Find(playlistId);

                if (song == null || playlist == null) return NotFound();

                if (playlist.Songs.Contains(song)) return View();

                playlist.Songs.Add(song);
                _context.Playlists.Update(playlist);

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return View();
            }
        }
    }
}
