using System;
using System.Collections.Generic;

namespace Shop_Dotnet2020.Models
{
    public partial class Chitiethoadon
    {
        public int Idcthd { get; set; }
        public int? Idhd { get; set; }
        public int? Quanlity { get; set; }
        public string Tensp { get; set; }
        public string Hinhanh { get; set; }
        public decimal? Gia { get; set; }

        public virtual Hoadon IdhdNavigation { get; set; }
    }
}
