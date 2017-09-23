using System.Collections.Immutable;
using System.Linq;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Enums.Configurations;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.UserInfoMappers
{
    public class UserListItemViewModelToUserInfoMapper : IMapper<UserListItemViewModel, UserInfo>
    {
        private readonly IRepository<Rating> _ratingRepository;

        public UserListItemViewModelToUserInfoMapper(IRepository<Rating> ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public UserInfo ConvertTo(UserListItemViewModel item)
        {
            throw new System.NotImplementedException();
        }

        public UserListItemViewModel ConvertFrom(UserInfo item)
        {
            var ratings =
                _ratingRepository.GetWhere(rating => item.Projects.Select(p => p.Id).Contains(rating.ProjectId));
            return new UserListItemViewModel
            {
                UserName = item.UserName,
                LastLoginTime = item.LastLoginTime.ToString(),
                RegistrationTime = item.RegistrationTime.ToString(),
                ProjectNumber = item.Projects.Count().ToString(),
                Raiting = (ratings.Any() ?
                _ratingRepository.GetWhere(rating => item.Projects.Select(p => p.Id).ToImmutableHashSet().Contains(rating.ProjectId))
                    .Sum (rating => rating.RatingResult) / (double) ratings.Count()
                    : 0).ToString(),
            Status = EnumConfiguration.StatusDisplayNames[item.Status],
                StatusCode = (int) item.Status,
                IsBlocked = item.IsBlocked,
                Avatar = item.Avatar
            };
        }
    }
}