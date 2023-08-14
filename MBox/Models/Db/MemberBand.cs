using System.ComponentModel.DataAnnotations;

namespace MBox.Models.Db
{
    public class MemberBand
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        public string? Info { get; set; }
        public List<Band> Bands { get; set; } = new();
    }
}
