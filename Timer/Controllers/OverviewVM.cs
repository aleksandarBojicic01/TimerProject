using Microsoft.AspNetCore.Identity;
using Timer.Models;

namespace Timer.Controllers.ViewModels
{
    public class OverviewVM
    {
       
        public List<TimeLog> TimeLogs { get; set; }
        public DateTime? StartDate { get;internal set; }
        public DateTime? EndDate { get; internal set; }
        public List<Customer> Customers { get; set; }
        public List<Category> Categories { get; set; }
        public List<Models.Task> Tasks { get; set; }
        public List<IdentityUser> Users { get; set; }
        public string SelectedCustomer { get; internal set; }
        public string SelectedCategory { get; internal set; }
        public string Hasvalue { get; internal set; }
     
        public string SelectedUser { get; internal set; }
    }
}