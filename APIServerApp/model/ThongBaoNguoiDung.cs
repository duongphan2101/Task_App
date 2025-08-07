using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIServerApp.Model
{
    [Table("ThongBaoNguoiDung")]
    public class ThongBaoNguoiDung
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaThongBao { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string NoiDung { get; set; }

        [ForeignKey("ChiTietCongViec")]
        public int MaChiTietCV { get; set; }

        [ForeignKey("NguoiDung")]
        public int MaNguoiDung { get; set; }

        public int TrangThai { get; set; }
        public DateTime NgayThongBao { get; set; }

        public virtual ChiTietCongViec ChiTietCongViec { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
    }
}