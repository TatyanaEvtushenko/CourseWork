using System;
using CourseWork.BusinessLogicLayer.ViewModels.NewsViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.NewsMappers
{
    public class NewsViewModelToNewsMapper : IMapper<NewsViewModel, News>
    {
        public News ConvertTo(NewsViewModel item)
        {
            throw new NotImplementedException();
        }

        public NewsViewModel ConvertFrom(News item)
        {
            return new NewsViewModel
            {
                Subject = item.Subject,
                Text = item.Text,
                Time = item.Time,
                Id = item.Id,
                ProjectName = item.Project?.Name
            };
        }
    }
}
