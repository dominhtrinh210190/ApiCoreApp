using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Services;

namespace Services
{
    public interface IServiceWrapper
    {
        public ICategoryRepository Category { get; set; }
        public IProductRepository Product { get; set; }
    }
    public class ServiceWrapper : IServiceWrapper
    {
        public ICategoryRepository Category { get; set; }
        public IProductRepository Product { get; set; }

        public ServiceWrapper(VpsDbContext dbContext)
        {
            Category = new CategoryRepository(dbContext);
            Product = new ProductRepository(dbContext);
        }
    }
}
