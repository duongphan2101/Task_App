using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APIServerApp.Model
{
    [Table("TepTin")]
    public class TepTin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTep { get; set; }

        [StringLength(255)]
        public string TenTep { get; set; }

        [StringLength(255)]
        public string TenTepGoc { get; set; }

        [StringLength(500)]
        public string DuongDan { get; set; }
        [JsonIgnore]
        public virtual ICollection<TepDinhKemEmail>? TepDinhKemEmails { get; set; } = new HashSet<TepDinhKemEmail>();
    }
}