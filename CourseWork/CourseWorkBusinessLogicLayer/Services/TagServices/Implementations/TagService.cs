using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.ViewModels.TagViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.TagServices.Implementations
{
    public class TagService : ITagService
    {
        private readonly Repository<Tag> _tagRepository;
        private readonly Repository<TagInProject> _tagInProjectRepository;
        private readonly IMapper<TagToAddingViewModel, Tag> _mapper;

        public TagService(Repository<Tag> tagRepository, Repository<TagInProject> tagInProjectRepository, IMapper<TagToAddingViewModel, Tag> mapper)
        {
            _tagRepository = tagRepository;
            _tagInProjectRepository = tagInProjectRepository;
            _mapper = mapper;
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

        public IEnumerable<TagToAddingViewModel> GetAllTagToAddingViewModels()
        {
            return _tagRepository.GetAll().Select(tag => _mapper.ConvertFrom(tag));
        }
    }
}
