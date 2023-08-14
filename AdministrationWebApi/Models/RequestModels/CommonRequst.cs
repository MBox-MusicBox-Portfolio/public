using System.ComponentModel.DataAnnotations;

namespace AdministrationWebApi.Models.RequestModels
{
    public class CommonRequst
    {
        [Required]
        public Guid IdObject { get; set; }
        [Required]
        public Guid? IdAdmin { get; set; }
        public string? Message { get; set; }
    }
}
