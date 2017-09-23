using CourseWork.BusinessLogicLayer.Services.AwardManagers;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.SearchManagers;
using CourseWork.BusinessLogicLayer.ViewModels.CommentViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.CommentManagers.Implementations
{
    public class CommentManager : ICommentManager
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<UserInfo> _userInfoRepository;
        private readonly ISearchManager _searchManager;
        private readonly IAwardManager _awardManager;
        private readonly IMapper<CommentFormViewModel, Comment> _commentMapper;
        private readonly IMapper<CommentViewModel, Comment> _commentViewMapper;

        public CommentManager(IRepository<Comment> commentRepository,
            IMapper<CommentFormViewModel, Comment> commentMapper, ISearchManager searchManager,
            IMapper<CommentViewModel, Comment> commentViewMapper, IAwardManager awardManager, IRepository<UserInfo> userInfoRepository)
        {
            _commentRepository = commentRepository;
            _commentMapper = commentMapper;
            _searchManager = searchManager;
            _commentViewMapper = commentViewMapper;
            _awardManager = awardManager;
            _userInfoRepository = userInfoRepository;
        }

        public CommentViewModel AddComment(CommentFormViewModel commentForm)
        {
            var comment = _commentMapper.ConvertTo(commentForm);
            if (!_commentRepository.AddRange(comment))
            {
                return null;
            }
            ProcessCommentAfterAdding(comment);
            return _commentViewMapper.ConvertFrom(comment);
        }

        public bool RemoveComment(string commentId)
        {
            var comment = _commentRepository.FirstOrDefault(c => c.Id == commentId);
            return _commentRepository.RemoveRange(commentId) && _searchManager.RemoveCommentsFromIndex(new[] {comment});
        }

        private void ProcessCommentAfterAdding(Comment comment)
        {
            _searchManager.AddCommentToIndex(comment);
            _awardManager.AddAwardForComments();
            comment.UserInfo = _userInfoRepository.FirstOrDefault(i => i.UserName == comment.UserName);
        }
    }
}
