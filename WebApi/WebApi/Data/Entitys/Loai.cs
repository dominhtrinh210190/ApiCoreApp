using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Data.Entitys
{
    [Table("Loai")]
    public class Loai
    {
        [Key]
        public int IDLoai { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenLoai { get; set; } 

        // chỉ định list bảng con
        public virtual ICollection<HangHoa> HangHoas { get; set; }
    }
}
