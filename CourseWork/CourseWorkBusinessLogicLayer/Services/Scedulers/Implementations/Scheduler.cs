using System;
using System.Collections.Generic;
using System.Linq;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.Scedulers.Implementations
{
    public class Scheduler : IScheduler
    {
        private readonly Repository<Project> _projectRepository;
        private readonly Repository<FinancialPurpose> _financialPurposeRepository;
        private readonly Repository<Payment> _paymentRepository;
        private readonly Repository<Rating> _ratingRepository;
        private readonly Repository<UserInfo> _userInfoRepository;

        public Scheduler(Repository<Project> projectRepository, Repository<FinancialPurpose> financialPurposeRepository,
            Repository<Payment> paymentRepository, Repository<Rating> ratingRepository, Repository<UserInfo> userInfoRepository)
        {
            _projectRepository = projectRepository;
            _financialPurposeRepository = financialPurposeRepository;
            _paymentRepository = paymentRepository;
            _ratingRepository = ratingRepository;
            _userInfoRepository = userInfoRepository;
        }

        public void UpdateData()
        {
            var projects = _projectRepository.GetAll();
            UpdateProjectStatuses(projects);
            UpdateRatings(projects);
            _projectRepository.UpdateRange(projects.ToArray());
        }
        
        public void ChangeProjectStatus(Project project, IEnumerable<Payment> payments, IEnumerable<FinancialPurpose> purposes)
        {
            if (IsFinancialProject(project, payments, purposes))
            {
                project.Status = ProjectStatus.Financed;
            }
            else
            {
                project.Status = project.FundRaisingEnd < DateTime.Today ? ProjectStatus.Failed : ProjectStatus.Active;
            }
        }

        private bool IsFinancialProject(Project project, IEnumerable<Payment> payments, IEnumerable<FinancialPurpose> purposes)
        {
            var projectPayments = payments.Where(payment => payment.ProjectId == project.Id);
            var lastPaymentTime = projectPayments.Any() ? projectPayments.Max(payment => payment.Time) : DateTime.Now;
            var minFinancialPurposeBudget = purposes.Where(purpose => purpose.ProjectId == project.Id)
                .Min(purpose => purpose.NecessaryPaymentAmount);
            return project.PaidAmount >= minFinancialPurposeBudget && project.FundRaisingEnd <= lastPaymentTime;
        }

        private void UpdateProjectStatuses(IEnumerable<Project> projects)
        {
            var payments = _paymentRepository.GetAll();
            var purposes = _financialPurposeRepository.GetAll();
            foreach (var project in projects)
            {
                ChangeProjectStatus(project, payments, purposes);
            }
        }

        private void UpdateRatings(IEnumerable<Project> projects)
        {
            UpdateProjectRatings(projects);
            UpdateUserRatings(projects);
        }

        private void UpdateProjectRatings(IEnumerable<Project> projects)
        {
            var ratings = _ratingRepository.GetAll();
            foreach (var project in projects)
            {
                var projectRaitings = ratings.Where(rating => rating.ProjectId == project.Id);
                project.Rating = !projectRaitings.Any() ? 0 : projectRaitings.Average(rating => rating.RatingResult);
            }
        }

        private void UpdateUserRatings(IEnumerable<Project> projects)
        {
            var userInfos = _userInfoRepository.GetAll();
            foreach (var userInfo in userInfos)
            {
                var userProjects = projects.Where(project => project.OwnerUserName == userInfo.UserName);
                userInfo.Rating = !userProjects.Any() ? 0 : userProjects.Average(project => project.Rating);
            }
            _userInfoRepository.UpdateRange(userInfos.ToArray());
        }
    }
}
