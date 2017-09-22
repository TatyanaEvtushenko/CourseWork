using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Services.NewsManagers;
using CourseWork.BusinessLogicLayer.ViewModels.NewsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    [Authorize(Roles = "ConfirmedUser, Admin")]
    public class NewsController : Controller
    {
        private readonly INewsManager _newsManager;

        public NewsController(INewsManager newsManager)
        {
            _newsManager = newsManager;
        }

        [HttpGet]
        [Route("api/News/GetLastNews")]
        [AllowAnonymous]
        public IEnumerable<NewsViewModel> GetLastNews()
        {
            return _newsManager.GetLastNews();
        }

        [HttpPost]
        [Route("api/News/AddNews")]
        public bool AddNews([FromBody]NewsFormViewModel newsForm)
        {
            return _newsManager.AddNews(newsForm);
        }

        [HttpPost]
        [Route("api/News/AddMailingToSubscribers")]
        public async Task<bool> AddMailingToSubscribers([FromBody]NewsFormViewModel newsForm)
        {
            return await _newsManager.AddMailingToSubscribers(newsForm);
        }

        [HttpPost]
        [Route("api/News/AddMailingToPayers")]
        public async Task<bool> AddMailingToPayers([FromBody]NewsFormViewModel newsForm)
        {
            return await _newsManager.AddMailingToPayers(newsForm);
        }

        [HttpPost]
        [Route("api/News/RemoveNews")]
        public bool RemoveNews([FromBody]string newsId)
        {
            return _newsManager.RemoveNews(newsId);
        }
    }
}