using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.DTO
{
    public class CongViecDuocGiaoDTO
    {
        public string MaCongViec { get; set; }
        public DateTime NgayGiao { get; set; }
        public string LapLai { get; set; }
        public string TanSuat { get; set; }
        public List<ChiTietCongViecDTO> ChiTiet { get; set; }
        public int NguoiGiao { get; set; }
        public NguoiLienQuanCongViecDto NguoiLienQuanCongViec { get; set; }
    }

        public class NguoiLienQuanCongViecDto
        {
            public string MaCongViec { get; set; }
            public int MaNguoiDung { get; set; }
            public string VaiTro { get; set; }
        }

}
