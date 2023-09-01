using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Timer.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Sequential Number")]
        public int SequentialNumber { get; set; }
        [Required]
        public string Name { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        [ValidateNever]
        public Customer Customer { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        public string IdentityUserId { get; set; }
        [DisplayName("Responsible")]
        [ForeignKey("IdentityUserId")]
        [ValidateNever]
        public IdentityUser IdentityUser { get; set; }
        [Required]
        [DisplayName("Estimated Hours")]
        public int EstimatedHours { get; set; }
        [Required]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
        [Required]
        [Range(1, 10)]
        public int Priority { get; set; }
    }
}
