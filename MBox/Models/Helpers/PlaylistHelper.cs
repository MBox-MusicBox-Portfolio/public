using MBox.Models.Db;
using System.ComponentModel.DataAnnotations;

namespace MBox.Models.Helpers
{
    public class PlaylistHelper // Used while creating new playlist
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPublic { get; set; } = false;
        [Required]
        public Guid AuthorId { get; set; }
    }
}
