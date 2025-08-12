using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIServerApp.Model
{
    [Table("ChucVu")]
    public class ChucVu
    {
        [Key]
        [StringLength(50)]
        public string? MaChucVu { get; set; }

        [Required]
        [StringLength(100)]
        public string? TenChucVu { get; set; }

        public virtual ICollection<NguoiDung>? NguoiDungs { get; set; }
    }
}