using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using WebApi.Models;

namespace WebApi.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly VpsDbContext dbcontext;
        public ProductRepository(VpsDbContext _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public List<ProductViewModel> GetAll()
        { 
            var list = dbcontext.Products.Select(a => new ProductViewModel
            {
                Name = a.Name
            });

            return list.ToList();
        }
    }
}
