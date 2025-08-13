using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_App.Model;

namespace Task_App.Response
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class TepTinResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public TepTin TepTin { get; set; } 
    }

    public class TepDinhKemEmailResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<TepDinhKemEmail> Data { get; set; }
    }
    public class PhanHoiCongViecResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<PhanHoiCongViec> PhanHoiCongViec { get; set; }
    }
    public class Object_Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
