using System;
using CourseWork.BusinessLogicLayer.Services.AwardManagers;
using CourseWork.BusinessLogicLayer.ViewModels.AwardViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.AwardMappers
{
    public class AwardSmallViewModelToAwardMapper : IMapper<AwardSmallViewModel, Award>
    {
        private readonly IAwardManager _awardManager;

        public AwardSmallViewModelToAwardMapper(IAwardManager awardManager)
        {
            _awardManager = awardManager;
        }

        public Award ConvertTo(AwardSmallViewModel item)
        {
            throw new NotImplementedException();
        }

        public AwardSmallViewModel ConvertFrom(Award item)
        {
            return new AwardSmallViewModel
            {
                Level = _awardManager.GetTrueLevelNumber(item.Level),
                Type = item.AwardType
            };
        }
    }
}
