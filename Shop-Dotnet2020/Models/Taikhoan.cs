using System;
using System.Collections.Generic;

namespace Shop_Dotnet2020.Models
{
    public partial class Taikhoan
    {
        public Taikhoan()
        {
            Feedbacks = new HashSet<Feedback>();
            Hoadons = new HashSet<Hoadon>();
            Logs = new HashSet<Log>();
        }

        public int Idkh { get; set; }
        public string Hoten { get; set; }
        public string Ngaysinh { get; set; }
        public string Gioitinh { get; set; }
        public string Cmnd { get; set; }
        public string Diachi { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
        public string Roles { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Hoadon> Hoadons { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
    }
}
