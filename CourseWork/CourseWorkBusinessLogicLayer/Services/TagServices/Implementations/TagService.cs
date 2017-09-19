using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.ViewModels.TagViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.TagServices.Implementations
{
    public class TagService : ITagService
    {
        private readonly Repository<Tag> _tagRepository;

        public TagService(Repository<Tag> tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public IEnumerable<TagViewModel> GetAllTagViewModels()
        {
            var tags = _tagRepository.GetAll();
            return tags.Select(tag => tag.Name).Distinct().Select(tag => new TagViewModel
            {
                Name = tag,
                NumberOfUsing = tags.Count(tagInProject => tagInProject.Name == tag)
            });
        }

        public IEnumerable<string> GetAllTagNames()
        {
            return _tagRepository.GetUnique(tag => tag.Name);
        }

        public IEnumerable<string> GetProjectTags(string projectId)
        {
            return _tagRepository.GetWhere(tag => tag.ProjectId == projectId).Select(tag => tag.Name);
        }
    }
}