using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServerApp.DTO
{
    public class CongViecDuocGiaoDto
    {
        public string? MaCongViec { get; set; }
        public DateTime NgayGiao { get; set; }
        public string? LapLai { get; set; }
        public string? TanSuat { get; set; }
        public List<ChiTietCongViecDto>? ChiTiet { get; set; }
        public int? NguoiGiao { get; set; }
        public NguoiLienQuanCongViecDto? NguoiLienQuanCongViec { get; set; }
    }

    public class NguoiDuocGiaoDto
    {
        public int MaNguoiDung { get; set; }
        public string? MaCongViec { get; set; }
        public string? VaiTro { get; set; }
    }
}