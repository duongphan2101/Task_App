using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.Model
{
    public class Email
    {
        public string MaEmail { get; set; }
        public int NguoiGui { get; set; }
        public int MaChiTietCV { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgayGui { get; set; }

        public int trangThai { get; set; }

        public ChiTietCongViec ChiTietCongViec { get; set; }
        public NguoiDung NguoiGuiObj { get; set; }
    }

}
