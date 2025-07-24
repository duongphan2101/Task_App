using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.Model
{
    public class CongViec
    {
        public string MaCongViec { get; set; }
        public int NguoiGiao { get; set; }
        public DateTime? NgayGiao { get; set; }
        public bool? LapLai { get; set; }
        public string TanSuat { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }

        public NguoiDung NguoiGiaoObj { get; set; }

    }

}
