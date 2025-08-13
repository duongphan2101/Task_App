using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIServerApp.Model
{
    [Table("PhanHoiCongViec")]
    public class PhanHoiCongViec
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaPhanHoi { get; set; }

        [StringLength(50)]
        public string MaCongViec { get; set; }

        [ForeignKey("NguoiDung")]
        public int MaNguoiDung { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string NoiDung { get; set; }

        public DateTime? ThoiGian { get; set; }

        [StringLength(50)]
        public string Loai { get; set; }

        [ForeignKey("MaCongViec")]
        public virtual CongViec CongViec { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}