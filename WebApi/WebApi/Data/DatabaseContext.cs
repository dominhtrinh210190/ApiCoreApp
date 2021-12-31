using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.Entitys;

namespace WebApi.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<HangHoa> HangHoas { get; set; }
        public DbSet<Loai> Loais { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<DonHangChiTiet> DonHangChiTiets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Property Configurations
            modelBuilder.Entity<DonHang>(e =>
            {
                e.ToTable("DonHang");
                e.HasKey(s => s.IDDonHang);
                e.Property(d=> d.NgayDat).HasDefaultValueSql("getutcdate()");
            });

            modelBuilder.Entity<DonHangChiTiet>(e =>
            {
                e.ToTable("DonHangChiTiet");
                e.HasKey(s => new { s.IDHangHoa, s.IDDonHang});

                e.HasOne(d => d.DonHang)
                .WithMany(a => a.DonHangChiTiets)
                .HasForeignKey(e => e.IDDonHang);

                e.HasOne(d => d.HangHoa)
                .WithMany(a => a.DonHangChiTiets)
                .HasForeignKey(e => e.IDHangHoa);
            });
        }
    }

    // chạy lệnh trong package manage console
    // b1. dotnet ef migrations add InitDbCreate -c DatabaseContext -s ./WebApi
    // b2. dotnet ef database update -s ./WebApi // để update xuống db 

    // FLUENT API thì cần phải ghi đè OnCreateTing 
}
