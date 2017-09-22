using System;
using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.MessageManagers;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.MessageViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

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

        private readonly Dictionary<AwardType, int[]> _levels = new Dictionary<AwardType, int[]>
        {
            [AwardType.ForComments] = new []{1, 5, 10, 30, 50, 80, 100, 200, 500},
            [AwardType.ForPayments] = new []{1, 10, 20, 50, 100, 200, 300, 500, 1000},
            [AwardType.ForReceivedPayments] = new []{1, 5, 10, 30, 50, 80, 100, 200, 500},
            [AwardType.ForSubscriptions] = new []{1, 5, 10, 20, 30, 50, 80, 100, 150},
            [AwardType.ForProjects] = new []{1, 3, 5, 10, 15, 20, 30, 50, 100},
        };

        public AwardManager(IUserManager userManager, IRepository<Award> awardRepository,IRepository<Comment> commentRepository,
            IRepository<Payment> paymentRepository, IRepository<ProjectSubscriber> subscriberRepository,
            IRepository<Project> projectRepository, IMessageManager messageManager)
        {
            _userManager = userManager;
            _awardRepository = awardRepository;
            _commentRepository = commentRepository;
            _paymentRepository = paymentRepository;
            _subscriberRepository = subscriberRepository;
            _projectRepository = projectRepository;
            _messageManager = messageManager;
        }

        public bool AddAwardForComments()
        {
            var ownerUserName = _userManager.CurrentUserName;
            return CheckAward(AwardType.ForComments, ownerUserName,
                () => _commentRepository.Count(c => c.UserName == ownerUserName));
        }

        public bool AddAwardForProjects()
        {
            var ownerUserName = _userManager.CurrentUserName;
            return CheckAward(AwardType.ForProjects, ownerUserName, 
                () => _projectRepository.Count(p => p.OwnerUserName == ownerUserName));
        }

        public bool AddAwardForPayments(Payment payment)
        {
            return CheckAward(AwardType.ForPayments, payment.UserName,
                () => payment.PaidAmount);
        }

        public bool AddAwardForReceivedSubscriptions(string projectId)
        {
            var ownerUserName = _projectRepository.FirstOrDefault(p => p.Id == projectId).OwnerUserName;
            return CheckAward(AwardType.ForSubscriptions, ownerUserName, 
                () => _subscriberRepository.Count(s => s.Project.OwnerUserName == ownerUserName));
        }

        public bool AddAwardForReceivedPayments(Project project)
        {
            return CheckAward(AwardType.ForReceivedPayments, project.OwnerUserName,
                () => _paymentRepository.GetWhere(p => p.Project.OwnerUserName == project.OwnerUserName)?.Sum(p => p.PaidAmount) ?? 0);
        }

        public decimal GetNeccessaryCountForAward(AwardType type, int level)
        {
            return _levels[type][level];
        }

        public int GetTrueLevelNumber(int level) => level + 1;

        private bool CheckAward(AwardType awardType, string ownerUserName, Func<decimal> countExistedValue)
        {
            var award = GetAward(awardType, ownerUserName);
            if (award == null)
            {
               return AddAward(awardType);
            }
            var isUpdated = UpdateAward(award, countExistedValue);
            SendMessageAboutNewAward(award, isUpdated, ownerUserName);
            return isUpdated;
        }

        private bool UpdateAward(Award award, Func<decimal> countExistedValue)
        {
            if (IsLastLevel(award))
            {
                return false;
            }
            var existedValue = countExistedValue();
            var existedLevelValue = _levels[award.AwardType].LastOrDefault(x => existedValue >= x);
            var existedLevel = (byte)Array.IndexOf(_levels[award.AwardType], existedLevelValue);
            return TryUpdateAwardInRepository(award, existedLevel);
        }

        private bool TryUpdateAwardInRepository(Award award, byte existedLevel)
        {
            if (award.Level >= existedLevel)
            {
                return false;
            }
            award.Level = existedLevel;
            return _awardRepository.UpdateRange(award);
        }

        private void SendMessageAboutNewAward(Award award, bool isUpdated, string recipientUserName)
        {
            if (isUpdated)
            {
                return;
            }
            var message = new MessageViewModel
            {
                RecipientUserName = recipientUserName,
                Text = GetMessageTextAfterNewAward(award)
            };
            _messageManager.Send(new []{message});
        }

        private bool IsLastLevel(Award award)
        {
            return award.Level >= _levels[award.AwardType].Length;
        }

        private bool AddAward(AwardType type)
        {
            var award = new Award
            {
                AwardType = type,
                UserName = _userManager.CurrentUserName
            };
            return _awardRepository.AddRange(award);
        }

        private Award GetAward(AwardType type, string ownerUserName)
        {
            return _awardRepository.FirstOrDefault(a => a.UserName == ownerUserName && a.AwardType == type);
        }

        private string GetMessageTextAfterNewAward(Award award) =>
            $"Вы получили награду \"{GetAwardName(award.AwardType)}\" {award.Level} уровня";

        private string GetAwardName(AwardType awardType)
        {
            switch (awardType)
            {
                case AwardType.ForComments:
                    return "Писатель";
                case AwardType.ForPayments:
                    return "Инвестор";
                case AwardType.ForSubscriptions:
                    return "Душа компании";
                case AwardType.ForProjects:
                    return "Создатель";
                case AwardType.ForReceivedPayments:
                    return "Бизнесмен";
                default:
                    throw new ArgumentOutOfRangeException(nameof(awardType), awardType, null);
            }
        }
    }
}
