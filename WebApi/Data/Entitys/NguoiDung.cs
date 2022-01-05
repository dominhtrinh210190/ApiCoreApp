using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entitys
{
    public class NguoiDung
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(50)]
        public string PassWord { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
    }
}
