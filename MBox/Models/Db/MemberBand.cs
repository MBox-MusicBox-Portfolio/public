using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MBox.Models.Db
{
    public class MemberBand
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public User? User { get; set; }
        public string? Info { get; set; }
        public bool IsEdit { get; set; } = false;
        [JsonIgnore]
        public List<Band> Bands { get; set; } = new();
    }
}
