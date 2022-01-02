using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class HangHoaModel
    {
        public int ID { get; set; } 
        public string TenHangHoa { get; set; }
        public string TenLoai { get; set; } 
        public double DonGia { get; set; }
        public int? IDLoai { get; set; }
        public string MoTa { get; set; }
    }
}
