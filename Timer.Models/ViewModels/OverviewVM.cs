using Microsoft.AspNetCore.Identity;
using Timer.Models;

namespace Timer.Models.ViewModels {
    public class OverviewVM
    {
       
        public List<TimeLog> TimeLogs { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
       
    }
}