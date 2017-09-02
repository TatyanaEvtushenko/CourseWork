using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.ProjectManagers.Implementations
{
    public class ProjectManager : IProjectManager
    {
        private readonly IRepository<Project> _projectRepository;
        private IMapper<Project, ProjectItemViewModel> _projectMapper;

        public ProjectManager(IRepository<Project> projectRepository, IMapper<Project, ProjectItemViewModel> projectMapper)
        {
            _projectRepository = projectRepository;
            _projectMapper = projectMapper;
        }

        public IEnumerable<ProjectItemViewModel> GetLastCreatedProjects()
        {
            //return _projectRepository.GetAll().OrderByDescending(project => project.CreatingTime).Take(10)
            //    .Select(project => _projectMapper.ConvertTo(project));
            return null;
        }

        public IEnumerable<ProjectItemViewModel> GetFinancedProjects()
        {
            return null; //_projectRepository.GetWhere();
        }
    }
}
