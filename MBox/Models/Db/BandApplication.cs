using System.ComponentModel.DataAnnotations;

namespace MBox.Models.Db
{
    public class BandApplication
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string? BandName { get; set; }
        public string? ApplicationText { get; set; }
        [Required]
        public User? Producer { get; set; }
        public string? ApplicationDate { get; set; }
        public ApplicationStatus? Status { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public DateTime? ChangedStatus { get; set; }
    }
}