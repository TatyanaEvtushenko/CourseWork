using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.Services.TagServices;
using CourseWork.BusinessLogicLayer.ViewModels.TagViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    [Route("api/HomeWebApi")]
    public class HomeWebApiController : Controller
    {
        private readonly ITagService _tagService;

        public HomeWebApiController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        [Route("~/api/HomeWebApi/tags")]
        public IEnumerable<TagViewModel> GetTagViewModels()
        {
            return _tagService.GetAllTagViewModels();
        }
    }
}