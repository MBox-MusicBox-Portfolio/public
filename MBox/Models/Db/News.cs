using System.ComponentModel.DataAnnotations;

namespace MBox.Models.Db
{
    public class News
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public string Body { get; set; }
        public byte[]? Poster { get; set; }
        public Band Author { get; set; }
    }
}
