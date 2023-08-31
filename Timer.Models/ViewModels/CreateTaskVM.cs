using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Timer.Models.ViewModels
{
    public class CreateTaskVM
    {
        public Task Task { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Customers { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Users { get; set; }

    }
}
