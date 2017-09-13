using System;
using CourseWork.BusinessLogicLayer.ViewModels.CommentViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class CommentViewModelToCommentMapper : IMapper<CommentViewModel, Comment>
    {
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
                UserName = item.UserName
            };
        }
    }
}
