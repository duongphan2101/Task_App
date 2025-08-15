using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIServerApp.Model
{
    [Table("ChiTietCongViec")]
    public class ChiTietCongViec
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaChiTietCV { get; set; }

        [ForeignKey("CongViec")]
        public string? MaCongViec { get; set; }

        [StringLength(255)]
        public string? TieuDe { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? NoiDung { get; set; }

        public DateTime? NgayNhanCongViec { get; set; }
        public DateTime? NgayKetThucCongViec { get; set; }
        public DateTime? NgayHoanThanh { get; set; }
        public int SoNgayHoanThanh { get; set; }

        public int TrangThai { get; set; }
        public int TienDo { get; set; }
        public int MucDoUuTien { get; set; }

        public virtual CongViec? CongViec { get; set; }

        public virtual ICollection<Email>? Emails { get; set; }

    }
}