using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Timer.Models;
using Timer.Models.Data;
using Timer.Models.ViewModels;
using Timer.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Newtonsoft.Json;

namespace Timer.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

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
            var storedVm = TempData["LoggerVM"] as LoggerVM;
            if (storedVm == null)
            {
                storedVm = loggerVM;
            }

            return View(storedVm);
        }
        [HttpPost]
        public void StartTimer([FromBody]LoggerVM? passedVm)
        {
            if (passedVm != null &&
                passedVm.TimeLog.CategoryId != 0 &&
                passedVm.TimeLog.CustomerId != 0 &&
                passedVm.TimeLog.TaskId != 0 &&
                passedVm.TimeLog.Notes != "")
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                passedVm.TimeLog.UserId = userId;
                passedVm.TimeLog.Date = DateTime.Now;
                passedVm.TimeLog.StartTime = DateTime.Now.TimeOfDay;
                TempData["LoggerVM"] = JsonConvert.SerializeObject(passedVm);
            }
        }

        [HttpPost]
        public IActionResult Index(LoggerVM passedVm)
        {
            var storedVmJson = TempData["LoggerVM"] as string;
            if (!string.IsNullOrEmpty(storedVmJson))
            {
                LoggerVM? storedVm = JsonConvert.DeserializeObject<LoggerVM>(storedVmJson);

                if (storedVm != null && !String.IsNullOrEmpty(storedVm.TimeLog.UserId))
                {
                    storedVm.TimeLog.EndTime = DateTime.Now.TimeOfDay;
                    storedVm.TimeLog.Duration = storedVm.TimeLog.EndTime - storedVm.TimeLog.StartTime;

                    if (ModelState.IsValid)
                    {
                        _db.TimeLogs.Add(storedVm.TimeLog);
                        _db.SaveChanges();
                        TempData["success"] = "Time Log entry added!";
                        storedVm = null;
                    }
                }
                else
                {
                    TempData["error"] = "Cannot stop timer without it being started!";
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