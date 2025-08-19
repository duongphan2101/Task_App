using Newtonsoft.Json;
using Task_App.Model;

namespace Task_App.DTO
{
    public class NguoiLienQuanDTO
    {
        public int maNguoiDung { get; set; }
        public string hoTen { get; set; }
        public string email { get; set; }
        public string tenDonVi { get; set; }
        public string tenPhongBan { get; set; }
        public string tenChucVu { get; set; }
        public string vaiTro { get; set; }
    }

    public class NguoiDungDTO
    {
        public int MaNguoiDung { get; set; }
        public string HoTen { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        public string MaDonVi { get; set; }
        public string MaPhongBan { get; set; }
    }

    public class NguoiDungLoginDTO
    {
        public int MaNguoiDung { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }

        public string MaPhongBan { get; set; }
        public string MaDonVi { get; set; }
        public string MaChucVu { get; set; }
        public bool? LaLanhDao { get; set; }

        public DonVi DonVi { get; set; }
        public PhongBan PhongBan { get; set; }
        public ChucVu ChucVu { get; set; }

    }

}
