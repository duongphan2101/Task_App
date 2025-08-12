using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIServerApp.Model
{
    [Table("NguoiDung")]
    public class NguoiDung
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNguoiDung { get; set; }

        [StringLength(100)]
        public string? HoTen { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(255)]
        public string MatKhau { get; set; }

        [StringLength(50)]
        public string? MaPhongBan { get; set; }

        [StringLength(50)]
        public string? MaDonVi { get; set; }

        [StringLength(50)]
        public string? MaChucVu { get; set; }

        public bool? LaLanhDao { get; set; }

        [ForeignKey("MaDonVi")]
        public virtual DonVi? DonVi { get; set; }

        [ForeignKey("MaPhongBan")]
        public virtual PhongBan? PhongBan { get; set; }

        [ForeignKey("MaChucVu")]
        public virtual ChucVu? ChucVu { get; set; }

        public virtual ICollection<CongViec>? CongViecs { get; set; }
        public virtual ICollection<NguoiLienQuanCongViec>? NguoiLienQuanCongViecs { get; set; }
        public virtual ICollection<PhanHoiCongViec>? PhanHoiCongViecs { get; set; }
        public virtual ICollection<NguoiNhanEmail>? NguoiNhanEmails { get; set; }
        public virtual ICollection<ThongBaoNguoiDung>? ThongBaoNguoiDungs { get; set; }
    }
}