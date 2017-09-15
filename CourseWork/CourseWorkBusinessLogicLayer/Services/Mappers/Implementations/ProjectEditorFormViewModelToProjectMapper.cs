using System;
using CourseWork.BusinessLogicLayer.Services.FinancialPurposeManagers;
using CourseWork.BusinessLogicLayer.Services.TagServices;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class ProjectEditorFormViewModelToProjectMapper : IMapper<ProjectEditorFormViewModel, Project>
    {
        private readonly ITagService _tagService;
        private readonly IFinancialPurposeManager _financialPurposeManager;

        public ProjectEditorFormViewModelToProjectMapper(IFinancialPurposeManager financialPurposeManager, ITagService tagService)
        {
            _financialPurposeManager = financialPurposeManager;
            _tagService = tagService;
        }

        public Project ConvertTo(ProjectEditorFormViewModel item)
        {
            throw new NotImplementedException();
        }

        public ProjectEditorFormViewModel ConvertFrom(Project item)
        {
            return new ProjectEditorFormViewModel
            {
                Description = item.Description,
                FinancialPurposes = _financialPurposeManager.GetProjectFinancialPurposees(item.Id),
                FundRaisingEnd = item.FundRaisingEnd,
                Id = item.Id,
                ImageUrl = item.ImageUrl,
                MaxPaymentAmount = item.MaxPayment,
                MinPaymentAmount = item.MinPayment,
                Name = item.Name,
                OwnerUserName = item.OwnerUserName,
                Tags = _tagService.GetProjectTags(item.Id)
            };
        }
    }
}
