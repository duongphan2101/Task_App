using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServerApp.DTO
{
    public class CongViecDaGiaoDto
    {
        public string? MaCongViec { get; set; }
        public DateTime NgayGiao { get; set; }
        public string? LapLai { get; set; }
        public string? TanSuat { get; set; }
        public List<ChiTietCongViecDto>? ChiTiet { get; set; }
        public NguoiNhanDto? NguoiNhan { get; set; }
    }

    public class ChiTietCongViecDto
    {
        public DateTime? NgayNhanCongViec { get; set; }
        public DateTime? NgayKetThucCongViec { get; set; }
        public int MaChiTietCV { get; set; }
        public string? TieuDe { get; set; }
        public string? NoiDung { get; set; }
        public DateTime? NgayHoanThanh { get; set; }
        public int? SoNgayHoanThanh { get; set; }
        public int TrangThai { get; set; }
        public int TienDo { get; set; }
        public int MucDoUuTien { get; set; }
    }

    public class NguoiNhanDto
    {
        public int MaNguoiDung { get; set; }
        public string? HoTen { get; set; }
        public string? Email { get; set; }
        public string? TenDonVi { get; set; }
        public string? TenPhongBan { get; set; }
        public string? TenChucVu { get; set; }
    }
}