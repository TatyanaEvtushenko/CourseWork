using System;
using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Options;
using CourseWork.BusinessLogicLayer.Services.MessageManagers;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.MessageViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
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
        private readonly AwardLevelOptions _awardOptions;

        public AwardManager(IUserManager userManager, IRepository<Award> awardRepository,
            IOptions<AwardLevelOptions> awardOptions, IRepository<Comment> commentRepository,
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
            _awardOptions = awardOptions.Value;
        }

        public bool AddAwardForComments()
        {
            var ownerUserName = _userManager.CurrentUserName;
            return CheckAward(AwardType.ForComments, ownerUserName, _awardOptions.CommentLevels, 
                () => _commentRepository.Count(c => c.UserName == ownerUserName));
        }

        public bool AddAwardForProjects()
        {
            var ownerUserName = _userManager.CurrentUserName;
            return CheckAward(AwardType.ForProjects, ownerUserName, _awardOptions.ProjectLevels, 
                () => _projectRepository.Count(p => p.OwnerUserName == ownerUserName));
        }

        public bool AddAwardForPayments(Payment payment)
        {
            return CheckAward(AwardType.ForPayments, payment.UserName, _awardOptions.PaymentLevels, 
                () => payment.PaidAmount);
        }

        public bool AddAwardForReceivedSubscriptions(string projectId)
        {
            var ownerUserName = _projectRepository.FirstOrDefault(p => p.Id == projectId).OwnerUserName;
            return CheckAward(AwardType.ForSubscriptions, ownerUserName, _awardOptions.SubscriptionLevels, 
                () => _subscriberRepository.Count(s => s.Project.OwnerUserName == ownerUserName));
        }

        public bool AddAwardForReceivedPayments(Project project)
        {
            return CheckAward(AwardType.ForReceivedPayments, project.OwnerUserName, _awardOptions.ReceivedPaymentLevels, 
                () => _paymentRepository.GetWhere(p => p.Project.OwnerUserName == project.OwnerUserName)?.Sum(p => p.PaidAmount) ?? 0);
        }

        private bool CheckAward(AwardType awardType, string ownerUserName, Dictionary<int, int> levels, Func<decimal> countExistedValue)
        {
            var award = GetAward(awardType, ownerUserName);
            if (award == null)
            {
               return AddAward(awardType);
            }
            var isUpdated = UpdateAward(award, levels, countExistedValue);
            SendMessageAboutNewAward(award, isUpdated, ownerUserName);
            return isUpdated;
        }

        private bool UpdateAward(Award award, Dictionary<int, int> levels, Func<decimal> countExistedValue)
        {
            if (IsLastLevel(award, levels))
            {
                return false;
            }
            var existedValue = countExistedValue();
            var existedLevel = (byte)levels.LastOrDefault(x => existedValue >= x.Value).Key;
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

        private bool IsLastLevel(Award award, Dictionary<int, int> levels)
        {
            return award.Level >= levels.Keys.Last();
        }

        private bool AddAward(AwardType type)
        {
            var award = new Award
            {
                AwardType = type,
                UserName = _userManager.CurrentUserName,
                Level = _awardOptions.FirstLevel
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
