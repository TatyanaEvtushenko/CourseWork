using System;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.RatingMappers
{
    public class RatingViewModelToRatingMapper : IMapper<RatingViewModel, Rating>
    {
        private readonly IUserManager _userManager;

        public RatingViewModelToRatingMapper(IUserManager userManager)
        {
            _userManager = userManager;
        }
        
        public Rating ConvertTo(RatingViewModel item)
        {
            return new Rating
            {
                ProjectId = item.ProjectId,
                RatingResult = item.RatingValue,
                UserName = _userManager.CurrentUserName,
                Id = Guid.NewGuid().ToString()
            };
        }

        public RatingViewModel ConvertFrom(Rating item)
        {
            throw new NotImplementedException();
        }
    }
}
