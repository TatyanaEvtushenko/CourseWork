using System;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.PaymentManagers;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using Microsoft.AspNetCore.Http;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class ProjectViewModelToProjectMapper : IMapper<ProjectViewModel, Project>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly Repository<Payment> _paymentRepository;
        private readonly Repository<Raiting> _raitingRepository;
        private readonly Repository<FinancialPurpose> _financialPurposeRepository;
        private readonly Repository<Tag> _tagRepository;
        private readonly IMapper<FinancialPurposeViewModel, FinancialPurpose> _financialPurposeMapper;

        public ProjectViewModelToProjectMapper(IHttpContextAccessor contextAccessor,
            Repository<Raiting> raitingRepository,
            IMapper<FinancialPurposeViewModel, FinancialPurpose> financialPurposeMapper,
            Repository<FinancialPurpose> financialPurposeRepository, Repository<Payment> paymentRepository, Repository<Tag> tagRepository)
        {
            _contextAccessor = contextAccessor;
            _raitingRepository = raitingRepository;
            _financialPurposeMapper = financialPurposeMapper;
            _financialPurposeRepository = financialPurposeRepository;
            _paymentRepository = paymentRepository;
            _tagRepository = tagRepository;
        }

        public Project ConvertTo(ProjectViewModel item)
        {
            throw new NotImplementedException();
        }

        public ProjectViewModel ConvertFrom(Project item)
        {
            var userName = _contextAccessor.HttpContext.User.Identity.Name;
            return new ProjectViewModel
            {
                Description = item.Description,
                FundRaisingEnd = item.FundRaisingEnd,
                MaxPaymentAmount = item.MaxPayment,
                MinPaymentAmount = item.MinPayment,
                Id = item.Id,
                ImageUrl = item.ImageUrl,
                Name = item.Name,
                CountOfPayments = _paymentRepository.Count(payment => payment.ProjectId == item.Id),
                PaidAmount = item.PaidAmount,
                Rating = _raitingRepository
                             .FirstOrDefault(rating => rating.ProjectId == item.Id && rating.UserName == userName)
                             ?.RaitingResult ?? 0,
                FinancialPurposes = _financialPurposeRepository.GetWhere(purpose => purpose.ProjectId == item.Id)
                    .Select(purpose => _financialPurposeMapper.ConvertFrom(purpose)),
                Tags = _tagRepository.GetWhere(tag => tag.ProjectId == item.Id).Select(tag => tag.Name),
                IsOwner = userName == item.OwnerUserName
            };
        }
    }
}