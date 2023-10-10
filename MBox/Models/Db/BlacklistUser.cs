using System.ComponentModel.DataAnnotations;

namespace MBox.Models.Db
{
    public class BlacklistUser
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public User? User { get; set; }
        public User? Admin { get; set; }
        public string? Messqge { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
