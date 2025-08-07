using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServerApp.Model
{
    public class ChiTietCongViec
    {
        public int MaChiTietCV { get; set; }
        public string MaCongViec { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgayNhanCongViec { get; set; }
        public DateTime? NgayKetThucCongViec{ get; set; }
        public DateTime? NgayHoanThanh {  get; set; }
        public int SoNgayHoanThanh { get; set; }
        public int TrangThai { get; set; }
        public int? TienDo { get; set; }
        public int MucDoUuTien { get; set; } // 0: bình thường, 1: quan trọng, 2: khẩn cấp
        public CongViec CongViec { get; set; }
    }

}
