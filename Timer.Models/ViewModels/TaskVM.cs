using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Timer.Models.ViewModels
{
    public class TaskVM
    {
        public List<Task> Tasks { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Category> Categories { get; set; }
        public List<Customer> Customers { get; set; }
        public List<IdentityUser> Users { get; set; }
        public string SelectedCustomer { get; set; }
        public string SelectedCategory { get; set; }
        public string SelectedUser { get; set; }

    }
}
