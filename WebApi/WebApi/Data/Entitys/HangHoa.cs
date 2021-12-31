using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Data.Entitys
{
    [Table("HangHoa")]
    public class HangHoa
    {
        public HangHoa()
        {
            DonHangChiTiets = new List<DonHangChiTiet>();
        }

        [Key]
        public int IDHangHoa { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenHH { get; set; }

        public string MoTa { get; set; }

        [Range(0, double.MaxValue)]
        public double DonGia { get; set; }

        public byte GiamGia { get; set; }
        public int? IDLoai { get; set; }

        // chỉ định rõ nó tham chiếu đến cột nào
        [ForeignKey("IDLoai")]
        public Loai Loai { get; set; }
        public virtual ICollection<DonHangChiTiet> DonHangChiTiets { get; set; }
    }
}
