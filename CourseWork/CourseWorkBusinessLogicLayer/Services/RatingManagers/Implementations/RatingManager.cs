using System.Linq;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.UserManagers;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.RatingManagers.Implementations
{
    public class RatingManager : IRatingManager
    {
        private readonly IRepository<Rating> _raitingRepository;
        private readonly IMapper<RatingViewModel, Rating> _ratingMapper;
        private readonly IUserManager _userManager;

        public RatingManager(IRepository<Rating> raitingRepository, IMapper<RatingViewModel, Rating> ratingMapper,
            IUserManager userManager)
        {
            _raitingRepository = raitingRepository;
            _ratingMapper = ratingMapper;
            _userManager = userManager;
        }

        public void ChangeRating(RatingViewModel ratingForm)
        {
            var ratingModel = _raitingRepository.FirstOrDefault(
                r => r.ProjectId == ratingForm.ProjectId && r.UserName == _userManager.CurrentUserName);
            if (ratingModel == null)
            {
                AddRating(ratingForm);
            }
            else
            {
                UpdateRating(ratingForm.RatingValue, ratingModel);
            }
        }

        public double GetProjectRatings(Project project)
        {
            var ratings = project.Ratings;
            if (ratings == null || !ratings.Any())
            {
                return 0;
            }
            return ratings.Average(r => r.RatingResult);
        }

        public double GetUserRatings(UserInfo info)
        {
            if (!info.Projects.Any())
            {
                return 0;
            }
            return info.Projects.Sum(p => p.Ratings.Average(r => r.RatingResult)) / info.Projects.Count();
        }

        private void AddRating(RatingViewModel ratingViewModel)
        {
            var ratingModel = _ratingMapper.ConvertTo(ratingViewModel);
            _raitingRepository.AddRange(ratingModel);
        }

        private void UpdateRating(int rating, Rating ratingModel)
        {
            ratingModel.RatingResult = rating;
            _raitingRepository.UpdateRange(ratingModel);
        }
    }
}
