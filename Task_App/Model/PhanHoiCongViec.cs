using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.Model
{
    public class PhanHoiCongViec
    {
        public int MaPhanHoi { get; set; }
        public string MaCongViec { get; set; }
        public int MaNguoiDung { get; set; }
        public string NoiDung { get; set; }
        public DateTime? ThoiGian { get; set; }
        public string Loai { get; set; }

        public CongViec CongViec { get; set; }
        public NguoiDung NguoiDung { get; set; }
    }

}
