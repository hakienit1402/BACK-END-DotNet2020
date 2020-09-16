using System;
using System.Collections.Generic;

namespace Shop_Dotnet2020.Models
{
    public partial class Feedback
    {
        public int Idfb { get; set; }
        public int? Idkh { get; set; }
        public int? Idsp { get; set; }
        public string Message { get; set; }
        public int? Rating { get; set; }
        public string Date { get; set; }

        public virtual Taikhoan IdkhNavigation { get; set; }
        public virtual Sanpham IdspNavigation { get; set; }
    }
}
