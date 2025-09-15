using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.Model
{
    public class Email
    {
        public string MaEmail { get; set; }
        public int NguoiGui { get; set; }
        public int MaChiTietCV { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime? NgayGui { get; set; }
        public int trangThai { get; set; }
        public string MessageId { get; set; }   // định danh duy nhất cho email này
        public string InReplyTo { get; set; }   // trỏ đến MessageId của email cha
        public string References { get; set; }  // chuỗi MessageId liên quan (cách nhau bởi dấu cách)


        public ChiTietCongViec ChiTietCongViec { get; set; }
        public NguoiDung NguoiGuiObj { get; set; }
    }

}
