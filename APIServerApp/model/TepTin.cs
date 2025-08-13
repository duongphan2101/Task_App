using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public virtual ICollection<TepDinhKemEmail> TepDinhKemEmails { get; set; }
    }
}