using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.DTO
{
    public class CongViecDuocGiaoDTO
    {
        // Thông tin công việc
        public string MaCongViec { get; set; }
        public DateTime NgayGiao { get; set; }
        public string LapLai { get; set; } // "Có" hoặc "Không"
        public string TanSuat { get; set; }

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

        // Người giao
        public string NguoiGiao_HoTen { get; set; }
        public string NguoiGiao_Email { get; set; }

        // Người nhận
        public int NguoiNhanID { get; set; }
        public string NguoiNhan_HoTen { get; set; }
        public string NguoiNhan_Email { get; set; }

        // Thông tin tổ chức của người giao
        public string TenDonVi { get; set; }
        public string TenPhongBan { get; set; }
        public string TenChucVu { get; set; }
    }

}
