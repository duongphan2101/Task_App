using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIServerApp.Model;

namespace APIServerApp.DTO
{
    public class ApiResponseDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }

    public class TepTinResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public TepTin? TepTin { get; set; }
    }

    public class FeedBackResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<PhanHoiCongViec>? PhanHoiCongViec { get; set; }
    }

    public class GetTepDinhKemResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<TepDinhKemEmail>? TepDinhKemEmail { get; set; }
    }

    public class GetNguoiDungCungDonViPhongBanResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<NguoiDungDTO>? NguoiDungs { get; set; }
    }

    public class Object_Response<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}