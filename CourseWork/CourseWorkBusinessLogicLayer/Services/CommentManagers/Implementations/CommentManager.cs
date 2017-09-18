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
        private readonly IMapper<CommentFormViewModel, Comment> _commentMapper;
        private readonly ISearchManager _searchManager;

        public CommentManager(Repository<Comment> commentRepository, IMapper<CommentFormViewModel, Comment> commentMapper, ISearchManager searchManager)
        {
            _commentRepository = commentRepository;
            _commentMapper = commentMapper;
            _searchManager = searchManager;
        }

        public string AddComment(CommentFormViewModel commentForm)
        {
            var comment = _commentMapper.ConvertTo(commentForm);
            var result = _commentRepository.AddRange(comment) && _searchManager.AddCommentToIndex(comment);
            return result ? comment.Id : null;
        }

        public bool RemoveComment(string commentId)
        {
            var comment = _commentRepository.Get(commentId);
            return _commentRepository.RemoveRange(commentId) && _searchManager.RemoveCommentsFromIndex(new [] { comment });
        }
    }
}
