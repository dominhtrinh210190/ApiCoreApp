using Data.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class VpsDbContext : DbContext
    {
        public VpsDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categorys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("Products");
                e.HasKey(a=> a.ID);
                e.HasOne(d => d.Categorys)
                .WithMany(a => a.Products)
                .HasForeignKey(e => e.IDCategory); // id khoa chinh cua bang cha
            });

            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("Categorys");
                e.HasKey(a=> a.ID);
            });
        }

        // chạy lệnh trong package manage console
        // b1. dotnet ef migrations add AddTables -c VpsDbContext -s ../WebApi
        // b2. dotnet ef database update -s ../WebApi // để update xuống db 
        // error : dotnet ef migrations remove -s ../WebApi -c VpsDbContext

        // FLUENT API thì cần phải ghi đè OnCreateTing 
    }
}
