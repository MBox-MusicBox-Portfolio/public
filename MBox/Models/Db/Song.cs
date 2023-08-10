using MBox.Models.Db;
using System.ComponentModel.DataAnnotations;

namespace MBox.Models.Db
{
    public class Song
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public byte[] Poster { get; set; }
        public string? Text { get; set; }
        public string? Description { get; set; }
        public string? Genre { get; set; }
        public Album? Album { get; set; }
        public List<Band> Author { get; set; }
        public int? Popularity { get; set; } // number of plays per week,/month
    }
}
