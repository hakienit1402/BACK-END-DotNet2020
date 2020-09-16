using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Shop_Dotnet2020.Models
{
    public partial class ShopDotnetContext : DbContext
    {
        public ShopDotnetContext()
        {
        }

        public ShopDotnetContext(DbContextOptions<ShopDotnetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Baiviet> Baiviets { get; set; }
        public virtual DbSet<Chitiethoadon> Chitiethoadons { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Hoadon> Hoadons { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Sanpham> Sanphams { get; set; }
        public virtual DbSet<Taikhoan> Taikhoans { get; set; }
        public virtual DbSet<Thuonghieu> Thuonghieux { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=HAKIENIT-DEV140\\SQLEXPRESS;Database=ShopDotnet;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Baiviet>(entity =>
            {
                entity.HasKey(e => e.Idbv)
                    .HasName("PK__Baiviet__9DB7DAD769B7764A");

                entity.ToTable("Baiviet");

                entity.Property(e => e.Idbv).HasColumnName("idbv");

                entity.Property(e => e.Anhchinh)
                    .HasColumnName("anhchinh")
                    .HasColumnType("text");

                entity.Property(e => e.Anhphu)
                    .HasColumnName("anhphu")
                    .HasColumnType("text");

                entity.Property(e => e.Ngay)
                    .HasColumnName("ngay")
                    .HasMaxLength(100);

                entity.Property(e => e.Noidung)
                    .HasColumnName("noidung")
                    .HasColumnType("text");

                entity.Property(e => e.Tenbv)
                    .HasColumnName("tenbv")
                    .HasMaxLength(100);

                entity.Property(e => e.Tieude)
                    .HasColumnName("tieude")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Chitiethoadon>(entity =>
            {
                entity.HasKey(e => e.Idcthd)
                    .HasName("PK__Chitieth__4EF63397945E9D2C");

                entity.ToTable("Chitiethoadon");

                entity.Property(e => e.Idcthd).HasColumnName("idcthd");

                entity.Property(e => e.Gia)
                    .HasColumnName("gia")
                    .HasColumnType("money");

                entity.Property(e => e.Hinhanh).HasColumnName("hinhanh");

                entity.Property(e => e.Idhd).HasColumnName("idhd");

                entity.Property(e => e.Quanlity).HasColumnName("quanlity");

                entity.Property(e => e.Tensp)
                    .HasColumnName("tensp")
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdhdNavigation)
                    .WithMany(p => p.Chitiethoadons)
                    .HasForeignKey(d => d.Idhd)
                    .HasConstraintName("fk_cthd_idhd");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.Idfb)
                    .HasName("PK__Feedback__9DB7BA4ED28BCE45");

                entity.ToTable("Feedback");

                entity.Property(e => e.Idfb).HasColumnName("idfb");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasMaxLength(100);

                entity.Property(e => e.Idkh).HasColumnName("idkh");

                entity.Property(e => e.Idsp).HasColumnName("idsp");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasMaxLength(100);

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.HasOne(d => d.IdkhNavigation)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.Idkh)
                    .HasConstraintName("fk_fbuser");

                entity.HasOne(d => d.IdspNavigation)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.Idsp)
                    .HasConstraintName("fk_fbsp");
            });

            modelBuilder.Entity<Hoadon>(entity =>
            {
                entity.HasKey(e => e.Idhd)
                    .HasName("PK__Hoadon__9DB78A0EA5744FF4");

                entity.ToTable("Hoadon");

                entity.Property(e => e.Idhd).HasColumnName("idhd");

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(1000);

                entity.Property(e => e.Idkh).HasColumnName("idkh");

                entity.Property(e => e.Ngay)
                    .HasColumnName("ngay")
                    .HasMaxLength(100);

                entity.Property(e => e.Tinhtrang)
                    .HasColumnName("tinhtrang")
                    .HasMaxLength(50);

                entity.Property(e => e.Tonggia)
                    .HasColumnName("tonggia")
                    .HasColumnType("money");

                entity.HasOne(d => d.IdkhNavigation)
                    .WithMany(p => p.Hoadons)
                    .HasForeignKey(d => d.Idkh)
                    .HasConstraintName("fk_hd_idkh");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(e => e.Idlog)
                    .HasName("PK__Log__07BE4DF81C386E09");

                entity.ToTable("Log");

                entity.Property(e => e.Idlog).HasColumnName("idlog");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasMaxLength(100);

                entity.Property(e => e.Idkh).HasColumnName("idkh");

                entity.Property(e => e.Ip)
                    .HasColumnName("ip")
                    .HasMaxLength(20);

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdkhNavigation)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.Idkh)
                    .HasConstraintName("fk_loguser");
            });

            modelBuilder.Entity<Sanpham>(entity =>
            {
                entity.HasKey(e => e.Idsp)
                    .HasName("PK__Sanpham__9DBB2CF209FE4CB3");

                entity.ToTable("Sanpham");

                entity.Property(e => e.Idsp).HasColumnName("idsp");

                entity.Property(e => e.Camera)
                    .HasColumnName("camera")
                    .HasMaxLength(100);

                entity.Property(e => e.Cauhinh)
                    .HasColumnName("cauhinh")
                    .HasMaxLength(100);

                entity.Property(e => e.Gia)
                    .HasColumnName("gia")
                    .HasColumnType("money");

                entity.Property(e => e.Hedieuhanh)
                    .HasColumnName("hedieuhanh")
                    .HasMaxLength(100);

                entity.Property(e => e.Hinhanh)
                    .HasColumnName("hinhanh")
                    .HasColumnType("text");

                entity.Property(e => e.Idth).HasColumnName("idth");

                entity.Property(e => e.Manhinh)
                    .HasColumnName("manhinh")
                    .HasMaxLength(100);

                entity.Property(e => e.Mausac)
                    .HasColumnName("mausac")
                    .HasMaxLength(100);

                entity.Property(e => e.Mota)
                    .HasColumnName("mota")
                    .HasMaxLength(2000);

                entity.Property(e => e.Pin)
                    .HasColumnName("pin")
                    .HasMaxLength(100);

                entity.Property(e => e.Sl).HasColumnName("sl");

                entity.Property(e => e.Tensp)
                    .HasColumnName("tensp")
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdthNavigation)
                    .WithMany(p => p.Sanphams)
                    .HasForeignKey(d => d.Idth)
                    .HasConstraintName("fk_spth");
            });

            modelBuilder.Entity<Taikhoan>(entity =>
            {
                entity.HasKey(e => e.Idkh)
                    .HasName("PK__Taikhoan__9DB77D6C6CD1B158");

                entity.ToTable("Taikhoan");

                entity.Property(e => e.Idkh).HasColumnName("idkh");

                entity.Property(e => e.Cmnd)
                    .HasColumnName("cmnd")
                    .HasMaxLength(20);

                entity.Property(e => e.Diachi)
                    .HasColumnName("diachi")
                    .HasMaxLength(200);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(200);

                entity.Property(e => e.Gioitinh)
                    .HasColumnName("gioitinh")
                    .HasMaxLength(5);

                entity.Property(e => e.Hoten)
                    .HasColumnName("hoten")
                    .HasMaxLength(500);

                entity.Property(e => e.Ngaysinh)
                    .HasColumnName("ngaysinh")
                    .HasMaxLength(11);

                entity.Property(e => e.Pass)
                    .HasColumnName("pass")
                    .HasMaxLength(20);

                entity.Property(e => e.Roles)
                    .HasColumnName("roles")
                    .HasMaxLength(20);

                entity.Property(e => e.Sdt)
                    .HasColumnName("sdt")
                    .HasMaxLength(11);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(1);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Thuonghieu>(entity =>
            {
                entity.HasKey(e => e.Idth)
                    .HasName("PK__Thuonghi__9DBB24DB71187C35");

                entity.ToTable("Thuonghieu");

                entity.Property(e => e.Idth).HasColumnName("idth");

                entity.Property(e => e.Logo)
                    .HasColumnName("logo")
                    .HasColumnType("text");

                entity.Property(e => e.Mota)
                    .HasColumnName("mota")
                    .HasMaxLength(2000);

                entity.Property(e => e.Tenth)
                    .HasColumnName("tenth")
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
