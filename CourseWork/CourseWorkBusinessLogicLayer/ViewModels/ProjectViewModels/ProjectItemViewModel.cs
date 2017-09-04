using System;

namespace CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels
{
    public class ProjectItemViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal PaidAmount { get; set; }

        public DateTime CreatingTime { get; set; }
    }
}
