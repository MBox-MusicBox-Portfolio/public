using System.ComponentModel.DataAnnotations;

namespace MBox.Models.Db
{
    public class News
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string? Name { get; set; }
        public Band? Author { get; set; }
        public MemberBand? Member { get; set; }
        public string? Poster { get; set; }
        public string? Link { get; set; }
        public string? Text { get; set; }

    }
}
