using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Timer.Models;
using Timer.Models.Data;
using Timer.Models.ViewModels;
using Timer.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Timer.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private static LoggerVM? vm;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<SelectListItem> CategoryList = _db.Categories.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            IEnumerable<SelectListItem> CustomerList = _db.Customers.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            IEnumerable<SelectListItem> TaskList = _db.Tasks.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            var loggerVM = new LoggerVM
            {
                TimeLog = new TimeLog(),
                Tasks = TaskList,
                Categories = CategoryList,
                Customers = CustomerList
            };
            if (vm == null)
            {
                vm = loggerVM;
            }

            return View(vm);
        }
        [HttpPost]
        public void StartTimer([FromBody]LoggerVM passedVm)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            passedVm.TimeLog.UserId = userId;
            passedVm.TimeLog.Date = DateTime.Now;
            passedVm.TimeLog.StartTime = DateTime.Now.TimeOfDay;
            vm = passedVm;
            TempData["success"] = "Timer started!";
        }

        [HttpPost]
        public IActionResult Index(LoggerVM passedVm)
        {
            if (vm != null)
            {
                vm.TimeLog.EndTime = DateTime.Now.TimeOfDay;
                vm.TimeLog.Duration = vm.TimeLog.EndTime - vm.TimeLog.StartTime;

                if (ModelState.IsValid)
                {
                    _db.TimeLogs.Add(vm.TimeLog);
                    _db.SaveChanges();
                    TempData["success"] = "Time Log entry added!";
                    vm = null;
                }
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}