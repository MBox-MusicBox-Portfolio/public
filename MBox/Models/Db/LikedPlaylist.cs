using System.ComponentModel.DataAnnotations;

namespace MBox.Models.Db
{
    public class LikedPlaylist
    {
        public Guid Id { get; set; }
        [Required]
        public User? User { get; set; }
        [Required]
        public Playlist? Playlist { get; set; }
    }
}
