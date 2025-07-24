using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.Model
{
    public class TepDinhKemEmail
    {
        public string MaEmail { get; set; }
        public int MaTep { get; set; }

        public Email Email { get; set; }
        public TepTin TepTin { get; set; }
    }

}
