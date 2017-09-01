using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.ViewModels.TagViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.TagServices.Implementations
{
    public class TagService : ITagService
    {
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<TagInProject> _tagInProjectRepository;

        public TagService(IRepository<Tag> tagRepository, IRepository<TagInProject> tagInProjectRepository)
        {
            _tagRepository = tagRepository;
            _tagInProjectRepository = tagInProjectRepository;
        }

        public IEnumerable<TagViewModel> GetAllTagViewModels()
        {
            var tagsInProject = _tagInProjectRepository.GetAll();
            return _tagRepository.GetAll().Select(tag => new TagViewModel
            {
                Name = tag.Name,
                NumberOfUsing = tagsInProject.Count(tagInProject => tagInProject.TagId == tag.Id)
            });
        }
    }
}
