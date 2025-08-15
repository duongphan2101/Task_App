using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_App.DTO;
using Task_App.Model;

namespace Task_App.Response
{
    public class LoginResponse
    {
        public string Token { get; set; }
        
        public int UserId { get; set; }
    }
}
