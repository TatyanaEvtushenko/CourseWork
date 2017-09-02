using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.Services.TagServices;
using CourseWork.BusinessLogicLayer.ViewModels.TagViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    [Route("api/Tag")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public IEnumerable<TagViewModel> GetTagCloud()
        {
            return _tagService.GetAllTagViewModels();
        }
    }
}