using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServerApp.DTO
{
    public class ChiTietCongViecResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public ChiTietCongViecFullDto? Data { get; set; }
    }

    public class NguoiLienQuanCongViecResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<NguoiLienQuanCongViecDTO>? Data { get; set; }
    }
}