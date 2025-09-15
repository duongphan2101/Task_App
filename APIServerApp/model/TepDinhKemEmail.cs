using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APIServerApp.Model
{
    [Table("TepDinhKemEmail")]
    public class TepDinhKemEmail
    {
        [Key, Column(Order = 0)]
        [StringLength(200)]
        public string? MaEmail { get; set; }

        [Key, Column(Order = 1)]
        public int? MaTep { get; set; }
        [JsonIgnore]
        [ForeignKey("MaEmail")]
        public virtual Email? Email { get; set; }

        [ForeignKey("MaTep")]
        public virtual TepTin? TepTin { get; set; }
    }
}