using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.Response
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public int MaNguoiDung { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string MaPhongBan { get; set; }
        public string MaDonVi { get; set; }
        public string MaChucVu { get; set; }
        public bool LaLanhDao { get; set; }
    }
}
