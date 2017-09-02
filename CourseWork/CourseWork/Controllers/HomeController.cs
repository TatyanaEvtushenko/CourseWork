using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Confirmed")]
        public IActionResult MyProjects()
        {
            ViewData["Message"] = "Here you can view your projects.";
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPage()
        {
            ViewData["Message"] = "Here you can view users.";
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
