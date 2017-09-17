using System;
using CourseWork.DataLayer.Enums;

namespace CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels
{
    public class ProjectItemViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string OwnerUserName { get; set; }

        public bool IsSubscriber { get; set; }

        public string Description { get; set; }

        public decimal PaidAmount { get; set; }

        public string ImageUrl { get; set; }

        public DateTime ProjectEndTime { get; set; }

        public ProjectStatus Status { get; set; }

        public double Rating { get; set; }
    }
}
