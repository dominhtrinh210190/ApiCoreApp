using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Data.Entitys;
using WebApi.Models;

namespace WebApi.Services
{
    public class HangHoaRepository : IHangHoaRepository
    {
        private readonly DatabaseContext dbcontext;
        public HangHoaRepository(DatabaseContext _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public HangHoaModel Add(HangHoaModel hangHoaVM)
        {
            var hanghoaEntity = new HangHoa
            {
                IDLoai = hangHoaVM.IDLoai,
                TenHH = hangHoaVM.TenHangHoa,
                MoTa = hangHoaVM.MoTa,
                DonGia = hangHoaVM.DonGia,
            };

            dbcontext.Add(hanghoaEntity);
            dbcontext.SaveChanges();

            hangHoaVM.ID = hanghoaEntity.IDHangHoa;
            return hangHoaVM;
        }
         
        public int Delete(int id)
        {
            var hanghoaEntity = dbcontext.HangHoas.SingleOrDefault(a=> a.IDHangHoa == id);
            if(hanghoaEntity != null)
            {
                dbcontext.Remove(hanghoaEntity);
                dbcontext.SaveChanges();
                return hanghoaEntity.IDHangHoa;
            }
            else
            {
                return 0;
            }
        }

        public List<HangHoaModel> GetAll()
        {
            var listHangHoaEntity = dbcontext.HangHoas.Select(a=> new HangHoaModel
            {
                TenHangHoa = a.TenHH,
                DonGia = a.DonGia,
                IDLoai = a.IDLoai,
                MoTa = a.MoTa,
                TenLoai = a.Loai.TenLoai
            });

            return listHangHoaEntity.ToList();
        }
         
        public HangHoaModel Update(HangHoaModel hangHoaVM)
        {
            var hanghoaEntity = dbcontext.HangHoas.SingleOrDefault(x => x.IDHangHoa == hangHoaVM.ID);
            if(hanghoaEntity != null)
            {
                hanghoaEntity.TenHH = hangHoaVM.TenHangHoa;
                hanghoaEntity.IDLoai = hangHoaVM.IDLoai;
                hanghoaEntity.DonGia = hangHoaVM.DonGia;
                hanghoaEntity.MoTa = hangHoaVM.MoTa;

                dbcontext.Update(hanghoaEntity);
                dbcontext.SaveChanges();

                return hangHoaVM;
            }
            else
            {
                return null;
            }
        }
    }
}
