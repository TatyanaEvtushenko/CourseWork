using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.SearchManagers;
using CourseWork.BusinessLogicLayer.ViewModels.CommentViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.CommentManagers.Implementations
{
    public class CommentManager : ICommentManager
    {
        private readonly Repository<Comment> _commentRepository;
        private readonly ISearchManager _searchManager;
        private readonly IMapper<CommentFormViewModel, Comment> _commentMapper;
        private readonly IMapper<CommentViewModel, Comment> _commentViewMapper;

        public CommentManager(Repository<Comment> commentRepository,
            IMapper<CommentFormViewModel, Comment> commentMapper, ISearchManager searchManager,
            IMapper<CommentViewModel, Comment> commentViewMapper)
        {
            _commentRepository = commentRepository;
            _commentMapper = commentMapper;
            _searchManager = searchManager;
            _commentViewMapper = commentViewMapper;
        }

        public CommentViewModel AddComment(CommentFormViewModel commentForm)
        {
            var comment = _commentMapper.ConvertTo(commentForm);
            if (!_commentRepository.AddRange(comment))
            {
                return null;
            }
            _searchManager.AddCommentToIndex(comment);
            return _commentViewMapper.ConvertFrom(comment);
        }

        public bool RemoveComment(string commentId)
        {
            var comment = _commentRepository.FirstOrDefault(c => c.Id == commentId);
            return _commentRepository.RemoveRange(commentId) && _searchManager.RemoveCommentsFromIndex(new[] {comment});
        }
    }
}
