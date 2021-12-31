using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Data.Entitys
{
    public class DonHangChiTiet
    { 
        public int IDDonHang { get; set; }
        public int IDHangHoa { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public byte GiamGia { get; set; }
        public double ThanhTien { get; set; }

        // relationship
        public DonHang DonHang { get; set; }    
        public HangHoa HangHoa { get; set; }    
    }
}
