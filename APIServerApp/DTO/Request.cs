using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServerApp.DTO
{
    public class DonViPhongBanRequest
    {
        public string MaDonVi { get; set; }
        public string MaPhongBan { get; set; }
        public string Email { get; set; }
    }

}