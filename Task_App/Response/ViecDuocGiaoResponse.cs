using System.Collections.Generic;
using Task_App.DTO;

namespace Task_App.Response
{
    public class ViecDuocGiaoResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<CongViecDuocGiaoDTO> Data { get; set; }
    }
}
