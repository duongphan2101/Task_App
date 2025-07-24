using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.Model
{
    public class MaCongViecSequence
    {
        public string MaDonVi { get; set; }
        public string MaPhongBan { get; set; }
        public int? STT { get; set; }

        public DonVi DonVi { get; set; }
        public PhongBan PhongBan { get; set; }
    }

}
