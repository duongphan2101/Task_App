using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.DTO
{
    public class CongViecChiTietDTO
    {
        // Thông tin công việc
        public string MaCongViec { get; set; }
        public DateTime NgayGiao { get; set; }
        public string LapLai { get; set; } // "Có" hoặc "Không"
        public string TanSuat { get; set; }

        // Thông tin người giao
        public string NguoiGiao_HoTen { get; set; }

        // Thông tin chi tiết công việc
        public DateTime NgayNhanCongViec { get; set; }
        public DateTime NgayKetThucCongViec { get; set; }
        public int MaChiTietCV { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgayHoanThanh { get; set; }
        public int? SoNgayHoanThanh { get; set; }
        public int TrangThai { get; set; }
        public int TienDo { get; set; }

        public int MucDoUuTien { get; set; } // 0: bình thường, 1: quan trọng, 2: khẩn cấp

        // Thông tin người nhận
        public int NguoiNhanID { get; set; }
        public string NguoiNhan_HoTen { get; set; }
        public string NguoiNhan_Email { get; set; }

        // Thông tin tổ chức của người giao
        public string TenDonVi { get; set; }
        public string TenPhongBan { get; set; }
        public string TenChucVu { get; set; }
    }

}
