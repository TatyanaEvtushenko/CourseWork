using CourseWork.BusinessLogicLayer.Services.AwardManagers;
using CourseWork.BusinessLogicLayer.Services.CommentManagers;
using CourseWork.BusinessLogicLayer.ViewModels.CommentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentManager _commentManager;
        private readonly IStringLocalizer<LocalizationController> _localizer;

        public CommentController(ICommentManager commentManager, IStringLocalizer<LocalizationController> localizer)
        {
            _commentManager = commentManager;
            _localizer = localizer;
        }

        [HttpPost]
        [Route("api/Comment/AddComment")]
        public CommentViewModel AddComment([FromBody]CommentFormViewModel comment)
        {
            var result = _commentManager.AddComment(comment);
            return result;
        }

        [HttpPost]
        [Route("api/Comment/RemoveComment")]
        public bool RemoveComment([FromBody]string commentId)
        {
            return _commentManager.RemoveComment(commentId);
        }
    }
}