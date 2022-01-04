using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entitys
{ 
    public class Category
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } 
        [ForeignKey("IDCategory")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
