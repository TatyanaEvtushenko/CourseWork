using System;
using CourseWork.BusinessLogicLayer.ViewModels.NewsViewModels;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.NewsMappers
{
    public class NewsViewModelToNewsMapper : IMapper<NewsViewModel, News>
    {
        private readonly IMapper<ProjectSmallInfoViewModel, Project> _projectMapper;

        public NewsViewModelToNewsMapper(IMapper<ProjectSmallInfoViewModel, Project> projectMapper)
        {
            _projectMapper = projectMapper;
        }

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
                ProjectInfo = item.Project != null ? _projectMapper.ConvertFrom(item.Project) : null,
            };
        }
    }
}
