using System;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;

namespace CourseWork.BusinessLogicLayer.ViewModels.CommentViewModels
{
    public class CommentViewModel
    {
        public string Id { get; set; }

        public UserSmallViewModel User { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }
    }
}
