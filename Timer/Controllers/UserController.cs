using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Timer.Models;
using Timer.Models.Data;

namespace Timer.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<IdentityUser> users = _db.Users.ToList();
            return View(users);
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<IdentityUser> users = _db.Users.ToList();
            return Json(new { data = users });
        }

        #endregion
    }
}
