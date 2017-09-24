using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CourseWork.BusinessLogicLayer.Options;
using CourseWork.BusinessLogicLayer.Services.MessageManagers;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.MessageViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace CourseWork.BusinessLogicLayer.Services.AwardManagers.Implementations
{
    public class AwardManager : IAwardManager
    {
        private readonly IUserManager _userManager;
        private readonly IMessageManager _messageManager;
        private readonly IRepository<Award> _awardRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<Payment> _paymentRepository;
        private readonly IRepository<ProjectSubscriber> _subscriberRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly Dictionary<AwardType, int[]> _levels;

        public AwardManager(IUserManager userManager, IRepository<Award> awardRepository,
            IRepository<Comment> commentRepository,
            IRepository<Payment> paymentRepository, IRepository<ProjectSubscriber> subscriberRepository,
            IRepository<Project> projectRepository, IMessageManager messageManager, IOptions<AwardOptions> options)
        {
            _userManager = userManager;
            _awardRepository = awardRepository;
            _commentRepository = commentRepository;
            _paymentRepository = paymentRepository;
            _subscriberRepository = subscriberRepository;
            _projectRepository = projectRepository;
            _messageManager = messageManager;
            _levels = new Dictionary<AwardType, int[]>
            {
                [AwardType.ForComments] = options.Value.ForComments,
                [AwardType.ForPayments] = options.Value.ForPayments,
                [AwardType.ForReceivedPayments] = options.Value.ForReceivedPayments,
                [AwardType.ForSubscriptions] = options.Value.ForSubscriptions,
                [AwardType.ForProjects] = options.Value.ForProjects
            };
        }

        public bool AddAwardForComments(string awardName)
        {
            var ownerUserName = _userManager.CurrentUserName;
            return CheckAward(AwardType.ForComments, ownerUserName,
                () => _commentRepository.Count(c => c.UserName == ownerUserName), awardName);
        }

        public bool AddAwardForProjects(string awardName)
        {
            var ownerUserName = _userManager.CurrentUserName;
            return CheckAward(AwardType.ForProjects, ownerUserName, 
                () => _projectRepository.Count(p => p.OwnerUserName == ownerUserName), awardName);
        }

        public bool AddAwardForPayments(Payment payment, string awardName)
        {
            return CheckAward(AwardType.ForPayments, payment.UserName,
                () => payment.PaidAmount, awardName);
        }

        public bool AddAwardForReceivedSubscriptions(string projectId, string awardName)
        {
            var ownerUserName = _projectRepository.FirstOrDefault(p => p.Id == projectId).OwnerUserName;
            return CheckAward(AwardType.ForSubscriptions, ownerUserName, 
                () => _subscriberRepository.Count(s => s.Project.OwnerUserName == ownerUserName, s => s.Project), awardName);
        }

        public bool AddAwardForReceivedPayments(Project project, string awardName)
        {
            return CheckAward(AwardType.ForReceivedPayments, project.OwnerUserName,
                () => _paymentRepository.GetWhere(p => p.Project.OwnerUserName == project.OwnerUserName, p => p.Project)?
                .Sum(p => p.PaidAmount) ?? 0, awardName);
        }

        public decimal GetNeccessaryCountForAward(AwardType type, int level)
        {
            return _levels[type][level];
        }

        public int GetTrueLevelNumber(int level) => level + 1;

        private bool CheckAward(AwardType awardType, string ownerUserName, Func<decimal> countExistedValue, string awardName)
        {
            var existedLevel = GetExistedLevel(awardType, countExistedValue);
            var award = GetAward(awardType, ownerUserName);
            bool isUpdated = false;
            if (award == null)
            {
                award = AddAward(awardType, existedLevel);
                if (award != null)
                    isUpdated = true;
            }
            else
            {
                isUpdated = UpdateAward(award, existedLevel);
            }
            //isUpdated = award == null ? AddAward(awardType, existedLevel) : UpdateAward(award, existedLevel);
            SendMessageAboutNewAward(award, isUpdated, ownerUserName, awardName);
            return isUpdated;
        }

        private byte GetExistedLevel(AwardType awardType, Func<decimal> countExistedValue)
        {
            var existedValue = countExistedValue();
            var existedLevelValue = _levels[awardType].LastOrDefault(x => existedValue >= x);
            return (byte)Array.IndexOf(_levels[awardType], existedLevelValue);
        }

        private bool UpdateAward(Award award, byte existedLevel)
        {
            if (IsLastLevel(award) || award.Level >= existedLevel)
            {
                return false;
            }
            award.Level = existedLevel;
            return _awardRepository.UpdateRange(award);
        }

        private void SendMessageAboutNewAward(Award award, bool isUpdated, string recipientUserName, string awardName)
        {
            if (!isUpdated)
            {
                return;
            }
            var message = new MessageViewModel
            {
                RecipientUserName = recipientUserName,
                Text = "AWARDNOTE",
                ParameterString = GetParameterString(award, awardName)
            };
            _messageManager.Send(new []{message});
        }

        private bool IsLastLevel(Award award)
        {
            return award.Level >= _levels[award.AwardType].Length;
        }

        private Award AddAward(AwardType type, byte existedLevel)
        {
            var award = new Award
            {
                AwardType = type,
                UserName = _userManager.CurrentUserName,
                Level = existedLevel
            };
            return _awardRepository.AddRange(award) ? award : null;
        }

        private Award GetAward(AwardType type, string ownerUserName)
        {
            return _awardRepository.FirstOrDefault(a => a.UserName == ownerUserName && a.AwardType == type);
        }

        private string GetParameterString(Award award, string awardName) =>
            awardName + "*" + GetTrueLevelNumber(award.Level);
    }
}
