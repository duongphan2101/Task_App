using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.DTO
{
    public class CongViecDaGiaoDTO
    {
        // Công việc
        public string MaCongViec { get; set; }
        public DateTime? NgayGiao { get; set; }
        public string LapLai { get; set; } // "Có" hoặc "Không"
        public string TanSuat { get; set; }

        // Chi tiết công việc
        public DateTime? NgayNhanCongViec { get; set; }
        public DateTime? NgayKetThucCongViec { get; set; }
        public int MaChiTietCV { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgayHoanThanh { get; set; }
        public int SoNgayHoanThanh { get; set; }
        public int TrangThai { get; set; }
        public string TienDo { get; set; }
        public int MucDoUuTien { get; set; } // 0: bình thường, 1: quan trọng, 2: khẩn cấp

        // Người nhận
        public int NguoiNhanID { get; set; }
        public string NguoiNhan_HoTen { get; set; }
        public string NguoiNhan_Email { get; set; }

        // Tổ chức
        public string TenDonVi { get; set; }
        public string TenPhongBan { get; set; }
        public string TenChucVu { get; set; }
    }

}
