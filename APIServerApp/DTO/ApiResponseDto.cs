using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServerApp.DTO
{
    public class ApiResponseDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}