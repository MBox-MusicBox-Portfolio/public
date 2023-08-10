using StackExchange.Redis;
using System.ComponentModel.DataAnnotations;

namespace MBox.Models.Db
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public List<Band>? FollowingsBands { get; set; } = new();
        public List<BandApplication>? BandApplications { get; set; } = new();
        [Required]
        public Role Role { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsBlocked { get; set; } = false;
    }
}
