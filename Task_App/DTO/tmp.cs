using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.DTO
{
    public static class tmp
    {
        public static string mk {  get; set; }
    }

    public static class TmpPass
    {
        public static string Pwd { get; set; }
    }

    public static class Duong_Dan
    {
        public static string DuongDan { get; set; }
        = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Attachments");
    }
}
