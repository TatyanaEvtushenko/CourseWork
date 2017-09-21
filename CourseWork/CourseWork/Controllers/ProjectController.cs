using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.ProjectManagers;
using CourseWork.BusinessLogicLayer.Services.RatingManagers;
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
        private readonly IRatingManager _ratingManager;

        public ProjectController(IProjectManager projectManager, IRatingManager ratingManager)
        {
            _projectManager = projectManager;
            _ratingManager = ratingManager;
        }

        [HttpGet]
        [Route("api/Project/GetFinancedProjects")]
        public IEnumerable<ProjectItemViewModel> GetFinancedProjects()
        {
            return _projectManager.GetFinancedProjects();
        }

        [HttpGet]
        [Route("api/Project/GetLastCreatedProjects")]
        public IEnumerable<ProjectItemViewModel> GetLastCreatedProjects()
        {
            return _projectManager.GetLastCreatedProjects();
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
            return _projectManager.GetCurrentUserProjects();
        }

        [HttpGet]
        [Route("api/Project/GetProjects")]
        [Authorize]
        public IEnumerable<ProjectItemViewModel> GetProjects([FromQuery] string username)
        {
            return _projectManager.GetUserProjects(username);
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
            _ratingManager.ChangeRating(rating);
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
            return _projectManager.GetSubscribedProjects().ToArray();
        }

        [HttpGet]
        [Route("api/Project/GetSubscribedProjects")]
        [Authorize]
        public ProjectItemViewModel[] GetUserSubscribedProjects([FromQuery] string username)
        {
            return _projectManager.GetUserSubscribedProjects(username).ToArray();
        }
    }
}