using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IHangHoaRepository
    {
        HangHoaModel Add(HangHoaModel hangHoaVM);
        HangHoaModel Update(HangHoaModel hangHoaVM);
        int Delete(int id);
        List<HangHoaModel> GetAll();
    }
}
