using Microsoft.AspNetCore.Mvc;
using Timer.Controllers.ViewModels;
using Timer.Models.Data;
using TimeLog = Timer.Models.TimeLog;
namespace Timer.Controllers
{
    public class OverviewController : Controller
    {
        private readonly ApplicationDbContext _db;
        public OverviewController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var OverviewVM = new OverviewVM
            {
                TimeLogs = _db.TimeLogs.ToList(),
                Customers = _db.Customers.ToList(),
                Categories = _db.Categories.ToList(),
                Tasks = _db.Tasks.ToList(),
                Users = _db.Users.ToList()
            };
            return View(OverviewVM);
        }
        //[HttpPost]
        //public IActionResult Index(OverviewVM viewModel)
        //{
        //    var timeLogsQuery = _db.TimeLogs.AsQueryable();

        //    if (!string.IsNullOrEmpty(viewModel.SelectedCustomer))
        //    {
        //        timeLogsQuery = timeLogsQuery.Where(t => t.CustomerId.ToString() == viewModel.SelectedCustomer);
        //    }

        //    if (!string.IsNullOrEmpty(viewModel.SelectedCategory))
        //    {
        //        timeLogsQuery = timeLogsQuery.Where(t => t.CategoryId.ToString() == viewModel.SelectedCategory);
        //    }

        //    if (viewModel.StartDate.HasValue || viewModel.EndDate.HasValue)
        //    {
        //        timeLogsQuery = timeLogsQuery.Where(t =>
        //                           (!viewModel.StartDate.HasValue || t.EndDate >= viewModel.StartDate) &&
        //                                              (!viewModel.EndDate.HasValue || t.StartDate <= viewModel.EndDate));
        //    }

        //    if (!string.IsNullOrEmpty(viewModel.SelectedUser))
        //    {
        //        timeLogsQuery = timeLogsQuery.Where(t => t.IdentityUserId == viewModel.SelectedUser);
        //    }

        //    viewModel.TimeLogs = timeLogsQuery.ToList();
        //    viewModel.Customers = _db.Customers.ToList();
        //    viewModel.Categories = _db.Categories.ToList();
        //    viewModel.Tasks = _db.Tasks.ToList();
        //    viewModel.Users = _db.Users.ToList();

        //    return View(viewModel);
        //}

    }
}
