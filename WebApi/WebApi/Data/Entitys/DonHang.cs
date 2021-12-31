using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Data.Entitys
{
    public enum TinhTrangDonHang
    {
        New = 0,
        Payment = 1,
        Complete = 2,
        Cancel = -1
    }
    public class DonHang
    {
        public DonHang()
        {
            DonHangChiTiets = new List<DonHangChiTiet>();
        }

        public int IDDonHang { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime? NgayGiao { get; set; }
        public string NguoiNhan { get; set; }
        public string DiaChiGiao { get; set; }
        public double TongTien { get; set; }
        public double SoDienThoai { get; set; }
        public TinhTrangDonHang TinhTrangDonHang { get; set; }

        public virtual ICollection<DonHangChiTiet> DonHangChiTiets { get; set; }
    }
}
