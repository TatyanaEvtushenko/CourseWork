using System.Collections.Generic;
using CourseWork.BusinessLogicLayer.Services.ProjectManagers;
using CourseWork.BusinessLogicLayer.ViewModels.ProjectViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectManager _projectManager;

        public ProjectController(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        [HttpPost]
        [Route("api/Project/AddProject")]
        [Authorize(Roles = "Admin, ConfirmedUser")]
        public bool AddProject([FromBody]ProjectFormViewModel projectForm)
        {
            return _projectManager.AddProject(projectForm);
        }

        [HttpGet]
        [Route("api/Project/GetUserProjects")]
        [Authorize(Roles = "Admin, ConfirmedUser")]
        public IEnumerable<ProjectItemViewModel> GetUserProjects()
        {
            return _projectManager.GetUserProjects();
        }

        [HttpGet]
        [Route("api/Project/GetProject/{id}")]
        public ProjectViewModel GetProject(string id)
        {
            return _projectManager.GetProject(id);
        }
    }
}