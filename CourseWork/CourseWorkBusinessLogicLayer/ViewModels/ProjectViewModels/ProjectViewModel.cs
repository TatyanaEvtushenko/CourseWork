using System;
using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.ViewModels.CommentViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.NewsViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Enums;

namespace CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels
{
    public class ProjectViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public double Rating { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public decimal PaidAmount { get; set; }

        public int CountOfPayments { get; set; }

        public IEnumerable<FinancialPurposeViewModel> FinancialPurposes { get; set; }

        public IEnumerable<string> Tags { get; set; }
        
        public DateTime FundRaisingEnd { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public IEnumerable<NewsViewModel> News { get; set; }

        public UserSmallViewModel Owner { get; set; }

        public ProjectStatus Status { get; set; }

        public bool IsSubscriber { get; set; }
    }
}
