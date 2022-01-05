using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface INguoiDungRepository
    {
        public List<NguoiDungViewModel> GetAll();

        public NguoiDungViewModel GetByUserNamePassWord(NguoiDungViewModel model);
    }
}
