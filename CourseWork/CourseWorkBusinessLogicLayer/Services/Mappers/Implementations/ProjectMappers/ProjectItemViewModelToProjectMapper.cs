using System;
using CourseWork.BusinessLogicLayer.Services.FinancialPurposesManagers;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.PaymentManagers;
using CourseWork.BusinessLogicLayer.Services.ProjectSubscriberManagers;
using CourseWork.BusinessLogicLayer.Services.RatingManagers;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.ProjectMappers
{
    public class ProjectItemViewModelToProjectMapper : IMapper<ProjectItemViewModel, Project>
    {
        private readonly IProjectSubscriberManager _projectSubscriberManager;
        private readonly IPaymentManager _paymentManager;
        private readonly IRatingManager _ratingManager;
        private readonly IFinancialPurposeManager _financialPurposeManager;

        public ProjectItemViewModelToProjectMapper(IProjectSubscriberManager projectSubscriberManager,
            IPaymentManager paymentManager, IRatingManager ratingManager, IFinancialPurposeManager financialPurposeManager)
        {
            _projectSubscriberManager = projectSubscriberManager;
            _paymentManager = paymentManager;
            _ratingManager = ratingManager;
            _financialPurposeManager = financialPurposeManager;
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
                Rating = _ratingManager.GetProjectRatings(item),
                Description = item.Description,
                OwnerUserName = item.OwnerUserName,
                ProjectEndTime = item.FundRaisingEnd,
                IsSubscriber = _projectSubscriberManager.IsSubscriber(item),
                PaidAmount = _paymentManager.GetProjectPaidAmount(item),
                NeccessaryAmount = _financialPurposeManager.GetProjectNeccessaryAmount(item)
            };
        }
    }
}
