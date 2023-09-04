using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timer.Models;
using Timer.Models.Data;
using Timer.Models.ViewModels;
using Timer.Utility;
using Task = Timer.Models.Task;

namespace Timer.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _db;
        private static TaskVM? vm;
        public TaskController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var taskVM = new TaskVM
            {
                Tasks = _db.Tasks.ToList(),
                Customers = _db.Customers.ToList(),
                Categories = _db.Categories.ToList(),
                Users = _db.Users.ToList(),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                SelectedCustomer = null,  
                SelectedCategory = null,
                SelectedUser = null
            };
            return View(taskVM);
        }

        [HttpPost]
        public IActionResult Index(TaskVM viewModel)
        {
            var tasksQuery = _db.Tasks.AsQueryable();

            if (!string.IsNullOrEmpty(viewModel.SelectedCustomer))
            {
                tasksQuery = tasksQuery.Where(t => t.CustomerId.ToString() == viewModel.SelectedCustomer);
            }

            if (!string.IsNullOrEmpty(viewModel.SelectedCategory))
            {
                tasksQuery = tasksQuery.Where(t => t.CategoryId.ToString() == viewModel.SelectedCategory);
            }

            if (viewModel.StartDate.HasValue || viewModel.EndDate.HasValue)
            {
                tasksQuery = tasksQuery.Where(t =>
                    (!viewModel.StartDate.HasValue || t.EndDate >= viewModel.StartDate) &&
                    (!viewModel.EndDate.HasValue || t.StartDate <= viewModel.EndDate));
            }

            if (!string.IsNullOrEmpty(viewModel.SelectedUser))
            {
                tasksQuery = tasksQuery.Where(t => t.IdentityUserId == viewModel.SelectedUser);
            }

            viewModel.Tasks = tasksQuery.ToList();

            viewModel.Customers = _db.Customers.ToList();
            viewModel.Categories = _db.Categories.ToList();
            viewModel.Users = _db.Users.ToList();

            vm = viewModel;

            return View(viewModel);
        }
        public IActionResult Create()
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
            IEnumerable<SelectListItem> UserList = _db.Users.Select(u => new SelectListItem
            {
                Text = u.UserName,
                Value = u.Id.ToString()
            });

            CreateTaskVM createTaskVm = new()
            {
                Task = new Task(),
                Categories = CategoryList,
                Customers = CustomerList,
                Users = UserList
            };
            return View(createTaskVm);
        }

        [HttpPost]
        public IActionResult Create(CreateTaskVM vm)
        {
            if (ModelState.IsValid)
            {
                _db.Tasks.Add(vm.Task);
                _db.SaveChanges();
                TempData["success"] = "Task added successfully!";
                
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return BadRequest();
            }

            Task? task = _db.Tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

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
            IEnumerable<SelectListItem> UserList = _db.Users.Select(u => new SelectListItem
            {
                Text = u.UserName,
                Value = u.Id.ToString()
            });

            CreateTaskVM vm = new()
            {
                Task = task,
                Categories = CategoryList,
                Customers = CustomerList,
                Users = UserList
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(CreateTaskVM vm)
        {
            if (ModelState.IsValid)
            {
                _db.Tasks.Update(vm.Task);
                _db.SaveChanges();
                TempData["success"] = "Task updated successfully!";
            }
            return RedirectToAction("Index");
        }

        

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Task> taskList;
            if (vm == null)
            {
               taskList = _db.Tasks.ToList();
            }
            else
            {
                taskList = vm.Tasks.ToList();
                vm = null;
            }

            foreach (var task in taskList)
            {
                task.Category = _db.Categories.FirstOrDefault(c => c.Id == task.CategoryId);
                task.Customer = _db.Customers.FirstOrDefault(c => c.Id == task.CustomerId);
                task.IdentityUser = _db.Users.FirstOrDefault(c => c.Id == task.IdentityUserId);
            }

            return Json(new { data = taskList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id <= 0 || id == null)
            {
                return BadRequest();
            }

            Task task = _db.Tasks.FirstOrDefault(u => u.Id == id);
            if (task == null)
            {
                return Json(new { success = false, message = "Error while deleting!" });
            }

            _db.Tasks.Remove(task);
            _db.SaveChanges();
            return Json(new { success = true, message = "Task deleted successfully!" });
        }

        #endregion
    }
}
