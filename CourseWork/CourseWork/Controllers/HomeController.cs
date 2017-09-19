using CourseWork.BusinessLogicLayer.ElasticSearch;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace CourseWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ElasticClient _client;

        public HomeController(SearchClient searchClient)
        {
            searchClient.CreateNewElasticClient();
            _client = searchClient.Client;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}