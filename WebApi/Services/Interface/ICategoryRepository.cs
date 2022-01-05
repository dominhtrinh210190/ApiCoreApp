using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

namespace Services.Interface
{
    public interface ICategoryRepository
    {
        public List<CategoryViewModel> GetAll();
    }
}
