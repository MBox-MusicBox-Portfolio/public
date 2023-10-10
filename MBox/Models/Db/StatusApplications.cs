using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MBox.Models.Db
{
    public class StatusApplications
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string? Name { get; set; }
        [JsonIgnore]
        public List<Application> Applications { get; set; } = new();
    }
}


