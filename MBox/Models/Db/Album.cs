using System.ComponentModel.DataAnnotations;

namespace MBox.Models.Db
{
    public class Album
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string? Poster { get; set; }
        public DateTime Release { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Band? Band { get; set; }
        public List<Song> Songs { get; set; } = new();
    }
}
