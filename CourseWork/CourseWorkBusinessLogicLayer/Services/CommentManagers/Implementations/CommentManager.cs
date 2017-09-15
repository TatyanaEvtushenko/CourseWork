using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.ViewModels.CommentViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.CommentManagers.Implementations
{
    public class CommentManager : ICommentManager
    {
        private readonly Repository<Comment> _commentRepository;
        private readonly IMapper<CommentFormViewModel, Comment> _commentMapper;

        public CommentManager(Repository<Comment> commentRepository, IMapper<CommentFormViewModel, Comment> commentMapper)
        {
            _commentRepository = commentRepository;
            _commentMapper = commentMapper;
        }

        public bool AddComment(CommentFormViewModel commentForm)
        {
            var comment = _commentMapper.ConvertTo(commentForm);
            return _commentRepository.AddRange(comment);
        }

        public bool RemoveComment(string commentId)
        {
            return _commentRepository.RemoveRange(commentId);
        }
    }
}
