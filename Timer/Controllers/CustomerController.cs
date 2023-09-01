using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timer.Models.Data;
using Timer.Models;
using Timer.Utility;

namespace Timer.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CustomerController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Customer> customerList = _db.Customers.ToList();
            return View(customerList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _db.Customers.Add(customer);
                _db.SaveChanges();
                TempData["success"] = "Customer created successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id <= 0 || id == null)
            {
                return BadRequest();
            }

            Customer customer = _db.Customers.FirstOrDefault(u => u.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _db.Customers.Update(customer);
                _db.SaveChanges();
                TempData["success"] = "Customer updated successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Customer> customerList = _db.Customers.ToList();
            return Json(new { data = customerList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id <= 0 || id == null)
            {
                return BadRequest();
            }

            Customer customer = _db.Customers.FirstOrDefault(u => u.Id == id);
            if (customer == null)
            {
                return Json(new { success = false, message = "Error while deleting!" });
            }

            _db.Customers.Remove(customer);
            _db.SaveChanges();
            return Json(new { success = true, message = "Customer deleted successfully!" });
        }

        #endregion
    }
}
