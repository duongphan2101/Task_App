using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace APIServerApp.DTO
{
    public class LoginReponse
    {
        public string? Token { get; set; }
        public int MaNguoiDung { get; set; }
        public string? HoTen { get; set; }
        public string? Email { get; set; }
        public string? MaPhongBan { get; set; }
        public string? MaDonVi { get; set; }
        public string? MaChucVu { get; set; }
        public bool? LaLanhDao { get; set; }

    }
}