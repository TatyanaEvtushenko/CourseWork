﻿using System;
using CourseWork.BusinessLogicLayer.Services.FinancialPurposesManagers;
using CourseWork.BusinessLogicLayer.Services.TagServices;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.ProjectMappers
{
    public class ProjectEditorFormViewModelToProjectMapper : IMapper<ProjectEditorFormViewModel, Project>
    {
        private readonly ITagService _tagService;
        private readonly IFinancialPurposeManager _financialPurposeManager;

        public ProjectEditorFormViewModelToProjectMapper(ITagService tagService, IFinancialPurposeManager financialPurposeManager)
        {
            _tagService = tagService;
            _financialPurposeManager = financialPurposeManager;
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
                FinancialPurposes = _financialPurposeManager.GetProjectFinancialPurposes(item),
                FundRaisingEnd = item.FundRaisingEnd,
                Id = item.Id,
                ImageUrl = item.ImageUrl,
                MaxPaymentAmount = item.MaxPayment,
                MinPaymentAmount = item.MinPayment,
                Name = item.Name,
                OwnerUserName = item.OwnerUserName,
                Tags = _tagService.GetProjectTags(item),
                AccountNumber = item.AccountNumber,
                Color = item.Color
            };
        }
    }
}
