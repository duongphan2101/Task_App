using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServerApp.DTO
{
    public class CongViecDuocGiaoResponseDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<CongViecDuocGiaoDto>? Data { get; set; }
    }
}