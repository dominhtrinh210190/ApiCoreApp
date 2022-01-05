using Data;
using Services.Interface;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;  

namespace Services.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly VpsDbContext dbcontext;
        public CategoryRepository(VpsDbContext _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public List<CategoryViewModel> GetAll()
        {
            var list = dbcontext.Categorys.Select(a => new CategoryViewModel
            {
                Name = a.Name
            });

            return list.ToList();
        }
    }
}
