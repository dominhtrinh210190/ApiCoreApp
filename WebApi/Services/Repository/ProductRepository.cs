using Data;
using Microsoft.EntityFrameworkCore;
using Services.Interface;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;  

namespace Services.Repository
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
