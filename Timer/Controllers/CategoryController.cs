using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timer.Models;
using Timer.Models.Data;
using Timer.Utility;

namespace Timer.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> categoryList = _db.Categories.ToList();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully!";
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

            Category category = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Category> categoryList = _db.Categories.ToList();
            return Json(new { data = categoryList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id <= 0 || id == null)
            {
                return BadRequest();
            }

            Category category = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (category == null)
            {
                return Json(new { success = false, message = "Error while deleting!" });
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();
            return Json(new { success = true, message = "Category deleted successfully!" });
        }


        #endregion
    }
}
