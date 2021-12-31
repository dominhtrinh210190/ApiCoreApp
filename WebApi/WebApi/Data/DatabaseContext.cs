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
    }

    // chạy lệnh trong package manage console
    // b1. dotnet ef migrations add InitDbCreate -c DatabaseContext -s ./WebApi
    // b2. dotnet ef database update -s ./WebApi // để update xuống db
}
