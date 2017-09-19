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
        private readonly IMapper<CommentViewModel, Comment> _commentViewMapper;

        public CommentManager(Repository<Comment> commentRepository,
            IMapper<CommentFormViewModel, Comment> commentMapper, IMapper<CommentViewModel, Comment> commentViewMapper)
        {
            _commentRepository = commentRepository;
            _commentMapper = commentMapper;
            _commentViewMapper = commentViewMapper;
        }

        public CommentViewModel AddComment(CommentFormViewModel commentForm)
        {
            var comment = _commentMapper.ConvertTo(commentForm);
            var result = _commentRepository.AddRange(comment);
            return _commentViewMapper.ConvertFrom(comment);
        }

        public bool RemoveComment(string commentId)
        {
            return _commentRepository.RemoveRange(commentId);
        }
    }
}
