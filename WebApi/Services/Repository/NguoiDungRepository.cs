using Data;
using Services.Interface;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class NguoiDungRepository : INguoiDungRepository
    {
        private readonly VpsDbContext dbcontext;
        public NguoiDungRepository(VpsDbContext _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public List<NguoiDungViewModel> GetAll()
        {
            var list = dbcontext.NguoiDungs.Select(a => new NguoiDungViewModel
            {
                UserName = a.UserName,
                PassWord = a.PassWord
            });

            return list.ToList();
        }

        public NguoiDungViewModel GetByUserNamePassWord(NguoiDungViewModel model)
        {
            var nguoidung = dbcontext.NguoiDungs.SingleOrDefault(a => a.UserName == model.UserName && a.PassWord == model.PassWord);
            if(nguoidung != null)
            {
                NguoiDungViewModel data = new NguoiDungViewModel 
                {
                    UserName = nguoidung.UserName,
                    PassWord = nguoidung.PassWord,
                    HoTen = nguoidung.HoTen,
                    Email = nguoidung.Email,
                    ID = nguoidung.ID,
                };
                return data;
            }
            else
            {
                return null;
            }
        }
    }
}
