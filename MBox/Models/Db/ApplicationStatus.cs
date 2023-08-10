using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace MBox.Models.Db
{
    public class ApplicationStatus
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }
    }
}


