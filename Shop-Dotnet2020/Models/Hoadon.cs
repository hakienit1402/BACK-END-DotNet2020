using System;
using System.Collections.Generic;

namespace Shop_Dotnet2020.Models
{
    public partial class Hoadon
    {
        public Hoadon()
        {
            Chitiethoadons = new HashSet<Chitiethoadon>();
        }

        public int Idhd { get; set; }
        public int? Idkh { get; set; }
        public string Diachi { get; set; }
        public string Ngay { get; set; }
        public decimal? Tonggia { get; set; }
        public string Tinhtrang { get; set; }

        public virtual Taikhoan IdkhNavigation { get; set; }
        public virtual ICollection<Chitiethoadon> Chitiethoadons { get; set; }
    }
}
