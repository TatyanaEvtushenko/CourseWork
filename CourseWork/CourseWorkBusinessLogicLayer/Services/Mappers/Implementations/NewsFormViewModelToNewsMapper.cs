using CourseWork.BusinessLogicLayer.ViewModels.NewsViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class NewsFormViewModelToNewsMapper : IMapper<NewsFormViewModel, News>
    {
        public News ConvertTo(NewsFormViewModel item)
        {
            return new News
            {
                ProjectId = item.ProjectId,
                Subject = item.Subject,
                Text = item.Text
            };
        }

        public NewsFormViewModel ConvertFrom(News item)
        {
            throw new System.NotImplementedException();
        }
    }
}
