using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServerApp.DTO
{
    public class ChiTietCongViecFullDto
    {
        public string MaCongViec { get; set; }
        public DateTime NgayGiao { get; set; }
        public string NguoiGiao_HoTen { get; set; }
        public string LapLai { get; set; }   // "Có" hoặc "Không"
        public string TanSuat { get; set; }

        public DateTime? NgayNhanCongViec { get; set; }
        public DateTime? NgayKetThucCongViec { get; set; }
        public int MaChiTietCV { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgayHoanThanh { get; set; }
        public int? SoNgayHoanThanh { get; set; }
        public int TrangThai { get; set; }
        public int? TienDo { get; set; }
        public int? MucDoUuTien { get; set; }

        public NguoiNhanDto NguoiNhan { get; set; }
    }

}