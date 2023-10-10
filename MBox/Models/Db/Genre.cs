using System.ComponentModel.DataAnnotations;

namespace MBox.Models.Db
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
    }
}
