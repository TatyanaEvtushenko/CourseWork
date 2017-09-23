using System;
using CourseWork.BusinessLogicLayer.Services.AwardManagers;
using CourseWork.BusinessLogicLayer.ViewModels.AwardViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.AwardMappers
{
    public class AwardViewModelToAwardMapper : IMapper<AwardViewModel, Award>
    {
        private readonly IAwardManager _awardManager;

        public AwardViewModelToAwardMapper(IAwardManager awardManager)
        {
            _awardManager = awardManager;
        }

        public Award ConvertTo(AwardViewModel item)
        {
            throw new NotImplementedException();
        }

        public AwardViewModel ConvertFrom(Award item)
        {
            return new AwardViewModel
            {
                NeccessaryCount = _awardManager.GetNeccessaryCountForAward(item.AwardType, item.Level),
                Level = _awardManager.GetTrueLevelNumber(item.Level),
                Type = item.AwardType
            };
        }
    }
}
