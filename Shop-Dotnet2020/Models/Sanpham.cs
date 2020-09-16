using System;
using System.Collections.Generic;

namespace Shop_Dotnet2020.Models
{
    public partial class Sanpham
    {
        public Sanpham()
        {
            Feedbacks = new HashSet<Feedback>();
        }

        public int Idsp { get; set; }
        public string Tensp { get; set; }
        public int? Idth { get; set; }
        public decimal? Gia { get; set; }
        public string Hinhanh { get; set; }
        public int? Sl { get; set; }
        public string Mota { get; set; }
        public string Mausac { get; set; }
        public string Manhinh { get; set; }
        public string Hedieuhanh { get; set; }
        public string Camera { get; set; }
        public string Cauhinh { get; set; }
        public string Pin { get; set; }

        public virtual Thuonghieu IdthNavigation { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
