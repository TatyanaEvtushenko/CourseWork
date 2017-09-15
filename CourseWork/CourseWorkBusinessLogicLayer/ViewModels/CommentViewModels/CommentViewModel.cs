using System;

namespace CourseWork.BusinessLogicLayer.ViewModels.CommentViewModels
{
    public class CommentViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }
    }
}
