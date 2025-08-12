using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIServerApp.Model
{
    [Table("Email")]
    public class Email
    {
        [Key]
        [StringLength(50)]
        public string MaEmail { get; set; }

        [ForeignKey("NguoiGuiObj")]
        public int NguoiGui { get; set; }

        [ForeignKey("ChiTietCongViec")]
        public int MaChiTietCV { get; set; }

        [StringLength(255)]
        public string? TieuDe { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? NoiDung { get; set; }

        public DateTime? NgayGui { get; set; }
        public int TrangThai { get; set; }

        public virtual ChiTietCongViec ChiTietCongViec { get; set; }
        public virtual NguoiDung NguoiGuiObj { get; set; }
        public virtual ICollection<NguoiNhanEmail> NguoiNhanEmails { get; set; }
        public virtual ICollection<TepDinhKemEmail> TepDinhKemEmails { get; set; }
    }
}