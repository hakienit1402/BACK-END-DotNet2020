using System;
using System.Collections.Generic;

namespace Shop_Dotnet2020.Models
{
    public partial class Thuonghieu
    {
        public Thuonghieu()
        {
            Sanphams = new HashSet<Sanpham>();
        }

        public int Idth { get; set; }
        public string Tenth { get; set; }
        public string Logo { get; set; }
        public string Mota { get; set; }

        public virtual ICollection<Sanpham> Sanphams { get; set; }
    }
}
