using System.ComponentModel.DataAnnotations;

namespace MBox.Models.Db
{
    public class Band
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public Producer Producer { get; set; }
        public List<MemberBand> Members { get; set; } = new();
        public List<Song> Songs { get; set; } = new();
        public List<Album> Albums { get; set; } = new();
        public DateTime? CreatedAt { get; set; }
        public string? Fullinfo { get; }
        public bool IsBlocked { get; set; }
    }
}
