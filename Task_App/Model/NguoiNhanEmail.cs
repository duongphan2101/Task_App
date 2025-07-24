using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.Model
{
    public class NguoiNhanEmail
    {
        public string MaEmail { get; set; }
        public int MaNguoiDung { get; set; }
        public string VaiTro { get; set; }

        public Email Email { get; set; }
        public NguoiDung NguoiDung { get; set; }
    }

}
