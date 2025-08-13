using System.Collections.Generic;
using Task_App.DTO;
using Task_App.Model;

namespace Task_App.Response
{
    public class ChiTietCongViecResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ChiTietCongViecFullDto Data { get; set; }
    }

    public class NguoiLienQuanCongViecResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<NguoiLienQuanDTO> Data { get; set; }
    }

    public class ChiTietResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ChiTietCongViec Data { get; set; }
    }
}
