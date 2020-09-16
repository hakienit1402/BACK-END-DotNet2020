using System;
using System.Collections.Generic;

namespace Shop_Dotnet2020.Models
{
    public partial class Log
    {
        public int Idlog { get; set; }
        public int? Idkh { get; set; }
        public string Message { get; set; }
        public string Ip { get; set; }
        public string Date { get; set; }

        public virtual Taikhoan IdkhNavigation { get; set; }
    }
}
