using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIServerApp.Model;

namespace APIServerApp.DTO
{
    public class DonViPhongBanRequest
    {
        public string? MaDonVi { get; set; }
        public string? MaPhongBan { get; set; }
        public string? Email { get; set; }
    }

    public class TaoCongViecRequest
    {
        public CongViec? CongViec { get; set; }
        public NguoiDung? NguoiDung { get; set; }
    }

    public class SendEmailRequest
    {
        public Email? Email { get; set; }
        public List<NguoiNhanEmail>? DanhSachNguoiNhanEmail { get; set; }
        public List<TepDinhKemEmail>? DanhSachTepDinhKem { get; set; }
        public NguoiDung? CurrentUser { get; set; }
        
    }



}