using CourseWork.BusinessLogicLayer.ViewModels.CommentViewModels;

namespace CourseWork.BusinessLogicLayer.Services.CommentManagers
{
    public interface ICommentManager
    {
        CommentViewModel AddComment(CommentFormViewModel commentForm, string awardName);

        bool RemoveComment(string commentId);
    }
}
