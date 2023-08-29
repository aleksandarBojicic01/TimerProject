using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timer.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(40)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public bool Billable { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public double VAT { get; set; }

    }
}
