using CourseWork.BusinessLogicLayer.ElasticSearch;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}