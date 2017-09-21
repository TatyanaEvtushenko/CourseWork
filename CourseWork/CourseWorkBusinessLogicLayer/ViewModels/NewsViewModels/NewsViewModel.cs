using System;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;

namespace CourseWork.BusinessLogicLayer.ViewModels.NewsViewModels
{
    public class NewsViewModel
    {
        public string Id { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }

        public ProjectSmallInfoViewModel ProjectInfo { get; set; }
    }
}
