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

    public class Dashboard_data
    {
        public int SoTaskTrongTuan { get; set; }
        public int SoTaskTrongThang { get; set; }
        public int SoTaskTrongNam { get; set; }
        public int SoTaskDaGiaoTrongTuan { get; set; }
        public int SoTaskDaGiaoTrongThang { get; set; }
        public int SoTaskDaGiaoTrongNam { get; set; }
        public int SoTaskChuaXuLiFillter { get; set; }
        public int SoTaskDangXuLiFillter { get; set; }
        public int SoTaskHoanThanhFillter { get; set; }
        public int SoTaskTreFillter { get; set; }
        public int SoTaskDaGiaoChuaXuLiFillter { get; set; }
        public int SoTaskDaGiaoDangXuLiFillter { get; set; }
        public int SoTaskDaGiaoHoanThanhFillter { get; set; }
        public int SoTaskDaGiaoTreFillter { get; set; }
    }

}
