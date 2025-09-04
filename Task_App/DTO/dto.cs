using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.DTO
{
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
        public string TrangThai { get; set; }
        public string TienDo { get; set; }
        public string MucDoUuTien { get; set; }
        public CongViecDto CongViec { get; set; }
    }

    public class CongViecDto
    {
        public string MaCongViec { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayGiao { get; set; }
        public DateTime? NgayKetThuc { get; set; }
    }

    public class NguoiDungView
    {
        public int MaNguoiDung { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string MaDonVi { get; set; }
        public string MaPhongBan { get; set; }
        public string MaChucVu { get; set; }
        public string DonVi { get; set; }
        public string PhongBan { get; set; }
        public string ChucVu { get; set; }
        public string TrangThai { get; set; }
    }

}
