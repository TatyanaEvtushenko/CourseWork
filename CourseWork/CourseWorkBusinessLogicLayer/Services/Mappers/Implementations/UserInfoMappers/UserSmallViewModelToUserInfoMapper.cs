using System;
using System.Linq;
using CourseWork.BusinessLogicLayer.ViewModels.AwardViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.UserInfoMappers
{
    public class UserSmallViewModelToUserInfoMapper : IMapper<UserSmallViewModel, UserInfo>
    {
        private readonly IMapper<AwardSmallViewModel, Award> _awardMapper;

        public UserSmallViewModelToUserInfoMapper(IMapper<AwardSmallViewModel, Award> awardMapper)
        {
            _awardMapper = awardMapper;
        }

        public UserInfo ConvertTo(UserSmallViewModel item)
        {
            throw new NotImplementedException();
        }

        public UserSmallViewModel ConvertFrom(UserInfo item)
        {
            return new UserSmallViewModel
            {
                UserName = item.UserName,
                Avatar = item.Avatar,
                Awards = item.Awards.Select(a => _awardMapper.ConvertFrom(a))
            };
        }
    }
}
