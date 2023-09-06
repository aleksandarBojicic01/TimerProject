using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timer.Models.Data;
using Timer.Models.ViewModels;
using Timer.Utility;
using TimeLog = Timer.Models.TimeLog;
namespace Timer.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class OverviewController : Controller
    {
        private readonly ApplicationDbContext _db;
        private static OverviewVM? vm;
        public OverviewController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var OverviewVM = new OverviewVM
            {
                TimeLogs = _db.TimeLogs.ToList(),
              
            };
            return View(OverviewVM);
        }
        [HttpPost]
        public IActionResult Index(OverviewVM viewModel)
        {
            var timeLogsQuery = _db.TimeLogs.AsQueryable();


            if (viewModel.StartDate.HasValue || viewModel.EndDate.HasValue)
            {
                timeLogsQuery = timeLogsQuery.Where(t =>
                                   (!viewModel.StartDate.HasValue || t.Date >= viewModel.StartDate) &&
                                                      (!viewModel.EndDate.HasValue || t.Date <= viewModel.EndDate));
            }

        
            viewModel.TimeLogs = timeLogsQuery.ToList();
            

            vm = viewModel;

            return View(viewModel);
        }



        [HttpGet]
        public IActionResult GetAll()
        {
            List<TimeLog> timeLogList;
            if (vm == null)
            {
                timeLogList = _db.TimeLogs.ToList();
            }
            else
            {
                timeLogList = vm.TimeLogs.ToList();
                vm = null;
            }
            
            foreach (var log in timeLogList)
            {                             
                log.Task = _db.Tasks.FirstOrDefault(t => t.Id == log.TaskId);
                log.Customer = _db.Customers.FirstOrDefault(c => c.Id == log.CustomerId);
                log.Category = _db.Categories.FirstOrDefault(c => c.Id == log.CategoryId);
            }

            return Json(new { data = timeLogList });
        }
    }
}