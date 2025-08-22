using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServerApp.DTO
{
    public class NguoiLienQuanCongViecDTO
    {
        public int MaNguoiDung { get; set; }
        public string? HoTen { get; set; }
        public string? Email { get; set; }
        public string? TenDonVi { get; set; }
        public string? TenPhongBan { get; set; }
        public string? TenChucVu { get; set; }
        public string? VaiTro { get; set; }
    }

    public class NguoiDungDTO
    {
        public int MaNguoiDung { get; set; }
        public string? HoTen { get; set; }
        public string? Email { get; set; }
        public string? MaDonVi { get; set; }
        public string? MaPhongBan { get; set; }
    }

    public class TaskDto
    {
        public int MaChiTietCV { get; set; }
        public string MaCongViec { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgayNhanCongViec { get; set; }
        public DateTime? NgayKetThucCongViec { get; set; }
        public DateTime? NgayHoanThanh { get; set; }
        public int? SoNgayHoanThanh { get; set; }
        public int TrangThai { get; set; }
        public int TienDo { get; set; }
        public int MucDoUuTien { get; set; }
        public CongViecDto CongViec { get; set; }
    }

    public class CongViecDto
    {
        public string MaCongViec { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayGiao { get; set; }
        public DateTime? NgayKetThuc { get; set; }
    }




}