using System;
using CourseWork.BusinessLogicLayer.ViewModels.CommentViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.CommentMappers
{
    public class CommentViewModelToCommentMapper : IMapper<CommentViewModel, Comment>
    {
        private readonly IMapper<UserSmallViewModel, UserInfo> _infoMapper;

        public CommentViewModelToCommentMapper(IMapper<UserSmallViewModel, UserInfo> infoMapper)
        {
            _infoMapper = infoMapper;
        }

        public Comment ConvertTo(CommentViewModel item)
        {
            throw new NotImplementedException();
        }

        public CommentViewModel ConvertFrom(Comment item)
        {
            return new CommentViewModel
            {
                Text = item.Text,
                Time = item.Time,
                Id = item.Id,
                User = _infoMapper.ConvertFrom(item.UserInfo)
            };
        }
    }
}
