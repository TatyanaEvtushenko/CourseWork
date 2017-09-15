using CourseWork.BusinessLogicLayer.ViewModels.CommentViewModels;

namespace CourseWork.BusinessLogicLayer.Services.CommentManagers
{
    public interface ICommentManager
    {
        bool AddComment(CommentFormViewModel commentForm);

        bool RemoveComment(string commentId);
    }
}
