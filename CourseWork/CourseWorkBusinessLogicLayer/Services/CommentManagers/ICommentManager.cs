using CourseWork.BusinessLogicLayer.ViewModels.CommentViewModels;

namespace CourseWork.BusinessLogicLayer.Services.CommentManagers
{
    public interface ICommentManager
    {
        CommentViewModel AddComment(CommentFormViewModel commentForm);

        bool RemoveComment(string commentId);
    }
}
