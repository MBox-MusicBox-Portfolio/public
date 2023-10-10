using System.ComponentModel.DataAnnotations;

namespace MBox.Models.Db
{
    public class Role
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string? Name { get; set; }
    }
}
