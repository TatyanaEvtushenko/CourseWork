using System;
using CourseWork.DataLayer.Enums;

namespace CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels
{
    public class ProjectItemViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal PaidAmount { get; set; }

        public DateTime CreatingTime { get; set; }

        public string ImageUrl { get; set; }

        public ProjectStatus Status { get; set; }
    }
}
