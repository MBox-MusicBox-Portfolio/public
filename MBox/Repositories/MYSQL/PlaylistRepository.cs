//using MBox.Models.Db;
//using MBox.Models.Presenters;
//using MBox.Repositories.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace MBox.Repositories.MYSQL;

//public class PlaylistRepository: IRepository<Playlist>
//{
//    private readonly AppDbContext _context;
//    private readonly ILogger? _logger;

//    public PlaylistRepository(AppDbContext context) { _context = context; }

//    public async Task<IEnumerable<Playlist>> GetAsync()
//    {
//        try
//        {
//            var playlists = await _context.Playlists.ToListAsync();
//            return playlists;
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "An error occurred while fetching playlists.");
//            return ErrorResult<IEnumerable<Playlist>>.Failed("An error occurred while fetching playlists.");
//        }
//    }

//    public async Task<ErrorResult<Playlist>> GetByIdAsync(Guid id)
//    {
//        try
//        {
//            var playlist = await _context.Playlists.FindAsync(id);
//            if (playlist == null)
//            {
//                return ErrorResult<Playlist>.NotFound("This playlist is not found");
//            }
//            return ErrorResult<Playlist>.Success(playlist);
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "An error occurred while fetching playlist.");
//            return ErrorResult<Playlist>.Failed("An error occurred while fetching playlist.");
//        }
//    }

//    public async Task<ErrorResult<Playlist>> AddAsync(Playlist playlist)
//    {
//        try
//        {
//            await _context.Playlists.AddAsync(playlist);
//            await _context.SaveChangesAsync();
//            return ErrorResult<Playlist>.Success(playlist);
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "An error occurred while adding the playlist.");
//            return ErrorResult<Playlist>.Failed("An error occurred while adding the playlist");
//        }
//    }

//    public ErrorResult<bool> UpdateAsync(Guid id, Playlist playlist)
//    {
//        try
//        {
//            var existingPlaylist = _context.Playlists.Find(id);
//            if (existingPlaylist == null)
//            {
//                return ErrorResult<bool>.NotFound("This playlist is not found");
//            }

//            // update of the properties.


//            // ... 

//            _context.SaveChanges();
//            return ErrorResult<bool>.Success(true);
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "An error occurred while updating the playlist.");
//            return ErrorResult<bool>.Failed("An error occurred while updating the playlist."); ;

//        }
//    }

//    public ErrorResult<bool> DeleteAsync(Guid id)
//    {
//        try
//        {
//            var playlist = _context.Playlists.Find(id);
//            if (playlist == null)
//            {
//                return ErrorResult<bool>.NotFound("This playlist is not found");
//            }

//            _context.Playlists.Remove(playlist);
//            _context.SaveChanges();
//            return ErrorResult<bool>.Success(true);
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "An error occurred while deleting the playlist.");
//            return ErrorResult<bool>.Failed("An error occurred while deleting the playlist."); ;
//        }
//    }
//}