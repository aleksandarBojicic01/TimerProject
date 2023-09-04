using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Timer.Models;
using Timer.Models.Data;
using Timer.Models.ViewModels;

namespace Timer.Controllers
{
    public class LoggerController : Controller
    {

        private readonly ApplicationDbContext _db;

        public LoggerController(ApplicationDbContext db)
        {
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
            return View(loggerVM);
        }

        [HttpPost]
        public IActionResult Index(LoggerVM vm)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            vm.TimeLog.UserId = userId;
            vm.TimeLog.Duration = vm.TimeLog.EndTime - vm.TimeLog.StartTime;
            
            if (ModelState.IsValid)
            {
                _db.TimeLogs.Add(vm.TimeLog);
                _db.SaveChanges();
                TempData["success"] = "Time log entry created successfully!";
            }
            return RedirectToAction("Index");
        }
    }
}
