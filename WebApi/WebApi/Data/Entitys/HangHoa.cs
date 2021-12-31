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
        [Key]
        public Guid ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenHH { get; set; }

        public string MoTa { get; set; }

        [Range(0, double.MaxValue)]
        public double DonGia { get; set; }

        public byte GiamGia { get; set; }
        public int? MaLoai { get; set; }

        // chỉ định rõ nó tham chiếu đến cột nào
        [ForeignKey("MaLoai")]
        public Loai Loai { get; set; }
    }
}
