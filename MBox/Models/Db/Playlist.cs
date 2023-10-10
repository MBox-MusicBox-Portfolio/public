using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MBox.Models.Db
{
    public class Playlist
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string? Name { get; set; }
        [Required]
        public User? Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Song> Songs { get; set; } = new();
        public bool IsPublic { get; set; }
    }
}
