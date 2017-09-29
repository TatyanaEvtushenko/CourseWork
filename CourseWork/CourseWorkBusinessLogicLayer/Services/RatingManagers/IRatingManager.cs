using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.RatingManagers
{
    public interface IRatingManager
    {
        void ChangeRating(RatingViewModel ratingForm);

        double GetProjectRatings(Project project);

        double GetUserRatings(UserInfo info);
    }
}
