using CourseWork.BusinessLogicLayer.ElasticSearch;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace CourseWork.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}