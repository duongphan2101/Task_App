using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServerApp.DTO
{
    public class CongViecDaGiaoResponseDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<CongViecDaGiaoDto>? Data { get; set; }
    }
}