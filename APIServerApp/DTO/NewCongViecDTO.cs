namespace APIServerApp.DTO
{
    public class NguoiGiaoRequestDto
    {
        public string? MaPhongBan { get; set; }
        public string? MaDonVi { get; set; }
    }

    public class CongViecRequestDto
    {
        public string? MaCongViec { get; set; }
        public int? NguoiGiao { get; set; }
        public DateTime NgayGiao { get; set; }
        public bool? LapLai { get; set; }
        public string? TanSuat { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }

        public List<ChiTietCongViecDtos>? ChiTietCongViecs { get; set; }
    }

    public class ChiTietCongViecDtos
    {
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayNhanCongViec { get; set; }
        public DateTime NgayKetThucCongViec { get; set; }
        public DateTime NgayHoanThanh { get; set; }
        public int SoNgayHoanThanh { get; set; }
        public int TrangThai { get; set; }
        public int TienDo { get; set; }
        public int MucDoUuTien { get; set; }

        public List<NguoiLienQuanDto>? NguoiLienQuans { get; set; }
        public List<EmailDto>? Emails { get; set; }
    }

    public class NguoiLienQuanDto
    {
        public int MaNguoiDung { get; set; }
        public string VaiTro { get; set; }
    }

    public class EmailDto
    {
        public int NguoiGui { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgayGui { get; set; }
        public int TrangThai { get; set; }
        public List<NguoiNhanEmailDto>? NguoiNhans { get; set; }
        public List<TepTinDto>? TepDinhKems { get; set; }
    }

    public class NguoiNhanEmailDto
    {
        public int MaNguoiDung { get; set; }
        public string VaiTro { get; set; }
    }

    public class TepTinDto
    {
        public string TenTep { get; set; }
        public string TenTepGoc { get; set; }
        public string DuongDan { get; set; }
    }
}
