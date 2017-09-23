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
        private IStringLocalizer<LocalizationController> _localizer;
        private readonly IAwardManager _awardManager;

        public CommentController(ICommentManager commentManager, IAwardManager awardManager, IStringLocalizer<LocalizationController> localizer)
        {
            _commentManager = commentManager;
            _awardManager = awardManager;
            _localizer = localizer;
        }

        [HttpPost]
        [Route("api/Comment/AddComment")]
        public CommentViewModel AddComment([FromBody]CommentFormViewModel comment)
        {
            var result = _commentManager.AddComment(comment);
            if (result != null)
            {
                System.Diagnostics.Debug.WriteLine("Hello: " + _localizer["COMMENTS_A"]);
            }
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