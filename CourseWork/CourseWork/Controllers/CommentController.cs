using CourseWork.BusinessLogicLayer.Services.CommentManagers;
using CourseWork.BusinessLogicLayer.ViewModels.CommentViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentManager _commentManager;

        public CommentController(ICommentManager commentManager)
        {
            _commentManager = commentManager;
        }

        [HttpPost]
        [Route("api/Comment/AddComment")]
        public CommentViewModel AddComment([FromBody]CommentFormViewModel comment)
        {
            return _commentManager.AddComment(comment);
        }

        [HttpPost]
        [Route("api/Comment/RemoveComment")]
        public bool RemoveComment([FromBody]string commentId)
        {
            return _commentManager.RemoveComment(commentId);
        }
    }
}