using System.Collections.Generic;
using System.Linq;
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
        [Route("api/Project/GetProjects")]
        [Authorize]
        public IEnumerable<ProjectItemViewModel> GetProjects([FromQuery] string username)
        {
            return _projectManager.GetProjects(username);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/Project/GetProject/{id}")]
        public ProjectViewModel GetProject(string id)
        {
            return _projectManager.GetProject(id);
        }

        [HttpPost]
        [Route("api/Project/ChangeRating")]
        public void ChangeRating([FromBody]RatingViewModel rating)
        {
            _projectManager.ChangeRating(rating);
        }

        [HttpGet]
        [Route("api/Project/GetProjectEditorForm/{id}")]
        [Authorize(Roles = "Admin, ConfirmedUser")]
        public ProjectEditorFormViewModel GetProjectEditorForm(string id)
        {
            return _projectManager.GetProjectEditorForm(id);
        }

        [HttpPost]
        [Route("api/Project/UpdateProject")]
        [Authorize(Roles = "Admin, ConfirmedUser")]
        public bool UpdateProject([FromBody]ProjectFormViewModel projectForm)
        {
            return _projectManager.UpdateProject(projectForm);
        }

        [HttpGet]
        [Route("api/Project/GetUserSubscribedProjects")]
        [Authorize]
        public ProjectItemViewModel[] GetUserSubscribedProjects()
        {
            return _projectManager.GetUserSubscribedProjects().ToArray();
        }

        [HttpGet]
        [Route("api/Project/GetSubscribedProjects")]
        [Authorize]
        public ProjectItemViewModel[] GetUserSubscribedProjects([FromQuery] string username)
        {
            return _projectManager.GetSubscribedProjects(username).ToArray();
        }
    }
}