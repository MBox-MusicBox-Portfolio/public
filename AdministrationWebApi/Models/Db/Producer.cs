using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AdministrationWebApi.Models.Db
{
    public class Producer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public User User { get; set; }
        [Required]
        [JsonIgnore]
        public List<Band> Bands { get; set; } = new();
    }
}
