using System.ComponentModel.DataAnnotations;

namespace MBox.Models.Db
{
    public class Album
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public byte[] Poster { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        public string? Description { get; set; }

        public List<Song> Songs { get; set; } = new();
        public Band Author { get; set; }
        public int? Popularity { get; set; } // number of plays per week,/month
    }
}
