using System;
using CourseWork.BusinessLogicLayer.Services.PaymentManagers;
using CourseWork.BusinessLogicLayer.Services.ProjectManagers;
using CourseWork.BusinessLogicLayer.Services.ProjectSubscriberManagers;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.ProjectMappers
{
    public class ProjectItemViewModelToProjectMapper : IMapper<ProjectItemViewModel, Project>
    {
        private readonly IProjectSubscriberManager _projectSubscriberManager;
        private readonly IPaymentManager _paymentManager;
        private readonly IProjectManager _projectManager;

        public ProjectItemViewModelToProjectMapper(IProjectSubscriberManager projectSubscriberManager,
            IPaymentManager paymentManager, IProjectManager projectManager)
        {
            _projectSubscriberManager = projectSubscriberManager;
            _paymentManager = paymentManager;
            _projectManager = projectManager;
        }

        public Project ConvertTo(ProjectItemViewModel item)
        {
            throw new NotImplementedException();
        }

        public ProjectItemViewModel ConvertFrom(Project item)
        {
            return new ProjectItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                ImageUrl = item.ImageUrl,
                Status = item.Status,
                Rating = _projectManager.GetProjectRating(item),
                Description = item.Description,
                OwnerUserName = item.OwnerUserName,
                ProjectEndTime = item.FundRaisingEnd,
                IsSubscriber = _projectSubscriberManager.IsSubscriber(item),
                PaidAmount = _paymentManager.GetProjectPaidAmount(item.Id, item.Payments)
            };
        }
    }
}
