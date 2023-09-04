using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Timer.Models
{
    public class TimeLog
    {
        [Key]
        public int Id { get; set; }
        [ValidateNever]
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        [ValidateNever]
        public Customer Customer { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        public int TaskId { get; set; }
        [ForeignKey("TaskId")]
        [ValidateNever]
        public Task Task { get; set; }
        [Required]
        [DisplayName("Start Time")]
        public TimeSpan StartTime { get; set; }
        [Required]
        [DisplayName("End Time")]
        public TimeSpan EndTime { get; set; }
        [Required] 
        public TimeSpan Duration { get; set; }
        [Required]
        public bool Billable { get; set; }
        public string Notes { get; set; }
    }
}
