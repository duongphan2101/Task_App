using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.Model
{
    public class NguoiLienQuanCongViec
    {
        public string MaCongViec { get; set; }
        public int MaNguoiDung { get; set; }
        public string VaiTro { get; set; } // 'to', 'cc', 'bcc'

        public CongViec CongViec { get; set; }
        public NguoiDung NguoiDung { get; set; }
    }

}
