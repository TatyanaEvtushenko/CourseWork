using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.ElasticSearch.Documents;
using CourseWork.BusinessLogicLayer.Services.SearchManagers;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    public class SearchController : Controller
    {
        private readonly ISearchManager _searchManager;

        public SearchController(ISearchManager searchManager)
        {
            _searchManager = searchManager;
        }

        [HttpGet]
        [Route("api/Search/Search")]
        public IEnumerable<ProjectItemViewModel> Search([FromQuery] string query)
        {
            if (String.IsNullOrEmpty(query))
                return Enumerable.Empty<ProjectItemViewModel>();
            return _searchManager.Search(query);
        }
    }
}