using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServerApp.DTO
{
    public class NguoiLienQuanCongViecDTO
    {
        public int MaNguoiDung { get; set; }
        public string? HoTen { get; set; }
        public string? Email { get; set; }
        public string? TenDonVi { get; set; }
        public string? TenPhongBan { get; set; }
        public string? TenChucVu { get; set; }
        public string? VaiTro { get; set; }
    }

}