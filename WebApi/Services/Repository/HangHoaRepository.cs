using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using WebApi.Models;

namespace WebApi.Services
{
    public class HangHoaRepository : IHangHoaRepository
    {
        private readonly VpsDbContext dbcontext;
        public HangHoaRepository(VpsDbContext _dbcontext)
        {
            dbcontext = _dbcontext;
        } 
    }
}
