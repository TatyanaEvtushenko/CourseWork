using System;
using System.Collections.Immutable;
using System.Linq;
using CourseWork.BusinessLogicLayer.ViewModels.AwardViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using Microsoft.Extensions.Localization;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.UserInfoMappers
{
    public class DisplayableInfoViewModelToUserInfoMapper : IMapper<DisplayableInfoViewModel, UserInfo>
    {
        private readonly IMapper<AwardViewModel, Award> _awardMapper;
        private readonly IRepository<Rating> _ratingRepository;

        public DisplayableInfoViewModelToUserInfoMapper(IMapper<AwardViewModel, Award> awardMapper, IRepository<Rating> ratingRepository)
        {
            _awardMapper = awardMapper;
            _ratingRepository = ratingRepository;
        }

        public UserInfo ConvertTo(DisplayableInfoViewModel item)
        {
            throw new NotImplementedException();
        }

        public DisplayableInfoViewModel ConvertFrom(UserInfo item)
        {
            var ratings =
                _ratingRepository.GetWhere(rating => item.Projects.Select(p => p.Id).Contains(rating.ProjectId));
            return new DisplayableInfoViewModel
            {
                UserName = item.UserName,
                RegistrationTime = item.RegistrationTime,
                Avatar = item.Avatar,
                About = item.About,
                ProjectNumber = item.Projects.Count(),
                Contacts = item.Contacts,
                Awards = item.Awards.Select(a => _awardMapper.ConvertFrom(a)),
                Rating = (ratings.Any() ?
                    _ratingRepository.GetWhere(rating => item.Projects.Select(p => p.Id).ToImmutableHashSet().Contains(rating.ProjectId))
                        .Sum(rating => rating.RatingResult) / (double)ratings.Count()
                    : 0)
            };
        }
    }
}
