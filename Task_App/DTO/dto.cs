using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_App.Model;

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

    public class ReplyEmailRequest
    {
        public Email Email { get; set; }
        public List<NguoiNhanEmail> DanhSachNguoiNhanEmail { get; set; }
        public List<TepDinhKemEmail> DanhSachTepDinhKem { get; set; }
        public NguoiDung CurrentUser { get; set; }
        public int TaskId { get; set; }
        public string MK { get; set; }

        public string InReplyTo { get; set; }   // Message-ID của email gốc
        public string References { get; set; }  // References (có thể trùng với InReplyTo hoặc chuỗi thread)
    }

    public class DownloadRequest
    {
        public string FilePath { get; set; }
        public string OriginalFileName { get; set; }
    }


}
