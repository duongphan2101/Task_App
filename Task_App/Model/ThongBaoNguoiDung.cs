using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.Model
{
    public class ThongBaoNguoiDung
    {
        public int MaThongBao { get; set; }
        public string NoiDung { get; set; }
        public int MaChiTietCV { get; set; }
        public int MaNguoiDung { get; set; }
        public int TrangThai { get; set; }
        public DateTime NgayThongBao { get; set; }

        public ChiTietCongViec ChiTietCongViec { get; set; }
        public NguoiDung NguoiDung { get; set; }
    }
}
