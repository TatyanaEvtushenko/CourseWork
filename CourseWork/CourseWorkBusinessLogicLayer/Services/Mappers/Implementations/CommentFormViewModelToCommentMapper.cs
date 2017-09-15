using System;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.CommentViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class CommentFormViewModelToCommentMapper : IMapper<CommentFormViewModel, Comment>
    {
        private readonly IUserManager _userManager;

        public CommentFormViewModelToCommentMapper(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public Comment ConvertTo(CommentFormViewModel item)
        {
            return new Comment
            {
                Id = Guid.NewGuid().ToString(),
                Text = item.Text,
                Time = DateTime.UtcNow,
                ProjectId = item.ProjectId,
                UserName = _userManager.CurrentUserName
            };
        }

        public CommentFormViewModel ConvertFrom(Comment item)
        {
            throw new NotImplementedException();
        }
    }
}
