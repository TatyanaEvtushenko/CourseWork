using System;
using CourseWork.BusinessLogicLayer.ViewModels.TagViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class TagToAddingViewModelToTagMapper : IMapper<TagToAddingViewModel, Tag>
    {
        public Tag ConvertTo(TagToAddingViewModel item)
        {
            throw new NotImplementedException();
        }

        public TagToAddingViewModel ConvertFrom(Tag item)
        {
            return new TagToAddingViewModel
            {
                Id = item.Id,
                Name = item.Name
            };
        }
    }
}
