using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AdministrationWebApi.Models.Db
{
    [Index(nameof(Name), IsUnique = true)]
    public class Role
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string? Name { get; set; }    
    }
}
