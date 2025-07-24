using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.Model
{
    public class NguoiDung
    {
        public int MaNguoiDung { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }

        public string MaPhongBan { get; set; }
        public string MaDonVi { get; set; }
        public string MaChucVu { get; set; }
        public bool? LaLanhDao { get; set; }

        // Optional navigation properties
        public DonVi DonVi { get; set; }
        public PhongBan PhongBan { get; set; }
        public ChucVu ChucVu { get; set; }
    }

}
