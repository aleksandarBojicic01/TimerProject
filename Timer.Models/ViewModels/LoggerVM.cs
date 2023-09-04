using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Timer.Models.ViewModels
{
    public class LoggerVM
    {
        public TimeLog TimeLog { get; set; }
        [ValidateNever] 
        public IEnumerable<SelectListItem> Categories { get; set; }
        [ValidateNever] 
        public IEnumerable<SelectListItem> Customers { get; set; }
        [ValidateNever] 
        public IEnumerable<SelectListItem> Tasks { get; set; }
    }

}
