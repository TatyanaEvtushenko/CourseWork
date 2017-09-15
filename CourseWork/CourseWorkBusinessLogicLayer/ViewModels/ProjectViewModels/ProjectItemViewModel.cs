using CourseWork.DataLayer.Enums;

namespace CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels
{
    public class ProjectItemViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string OwnerUserName { get; set; }

        public string Description { get; set; }

        public decimal PaidAmount { get; set; }

        public string ImageUrl { get; set; }

        public ProjectStatus Status { get; set; }

        public double Raiting { get; set; }
    }
}
