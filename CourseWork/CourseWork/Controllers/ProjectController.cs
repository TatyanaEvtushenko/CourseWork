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
    }
}