using System.ComponentModel.DataAnnotations;

namespace MBox.Models.Db
{
    public class Producer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public User User { get; set; }
        [Required]
        public List<Band> Bands { get; set; } = new();
    }
}