using System.ComponentModel.DataAnnotations;

namespace MBox.Models.Db
{
    public class SocialUserCredential
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public User? User { get; set; }
        public string? SocialNetwork { get; set; }
        public string? SocialUserID { get; set; }
    }
}
