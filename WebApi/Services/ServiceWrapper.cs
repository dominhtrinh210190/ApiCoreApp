using Data;
using Services.Interface;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Services
{
    public interface IServiceWrapper
    {
        public ICategoryRepository Category { get; set; }
        public IProductRepository Product { get; set; }
        public INguoiDungRepository NguoiDung { get; set; }
        public IRefreshTokenRepository RefreshToken { get; set; }
    }
    public class ServiceWrapper : IServiceWrapper
    {
        public ICategoryRepository Category { get; set; }
        public IProductRepository Product { get; set; }
        public INguoiDungRepository NguoiDung { get; set; }
        public IRefreshTokenRepository RefreshToken { get; set; }

        public ServiceWrapper(VpsDbContext dbContext)
        {
            Category = new CategoryRepository(dbContext);
            Product = new ProductRepository(dbContext);
            NguoiDung = new NguoiDungRepository(dbContext);
            RefreshToken = new RefreshTokenRepository(dbContext);
        }
    }
}
