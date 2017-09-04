using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.ProjectManagers.Implementations
{
    public class ProjectManager : IProjectManager
    {
        private readonly Repository<Project> _projectRepository;
        private readonly IMapper<ProjectItemViewModel, Project> _projectMapper;

        public ProjectManager(Repository<Project> projectRepository, IMapper<ProjectItemViewModel, Project> projectMapper)
        {
            _projectRepository = projectRepository;
            _projectMapper = projectMapper;
        }

        public bool AddProject(ProjectFormViewModel projectForm)
        {
            return false; //_projectRepository.AddRange()
        }

        public IEnumerable<ProjectItemViewModel> GetLastCreatedProjects()
        {
            return _projectRepository.GetAll().OrderByDescending(project => project.CreatingTime).Take(10)
                .Select(project => _projectMapper.ConvertFrom(project));
        }

        public IEnumerable<ProjectItemViewModel> GetFinancedProjects()
        {
            return _projectRepository.GetWhere(project => project.Status == ProjectStatus.Financed)
                .Select(project => _projectMapper.ConvertFrom(project));
        }
    }
}
