using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_App.Model
{
    internal class RequestWrapper
    {
        public string Command { get; set; }
        public object Data { get; set; }
    }
}
