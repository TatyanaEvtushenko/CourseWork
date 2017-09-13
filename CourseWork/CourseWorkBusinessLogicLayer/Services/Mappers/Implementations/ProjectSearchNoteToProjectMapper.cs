using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.ElasticSearch.Documents;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using Nest;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations
{
    public class ProjectSearchNoteToProjectMapper : IMapper<ProjectSearchNote, Project>
    {
        private readonly Repository<Tag> _tagRepository;
        private readonly Repository<News> _newsRepository;
        private readonly Repository<Comment> _commentRepository;
        private readonly Repository<FinancialPurpose> _financialPurposeRepository;

        public ProjectSearchNoteToProjectMapper(Repository<Tag> tagRepository, Repository<News> newsRepository, Repository<Comment> commentRepository, Repository<FinancialPurpose> financialPurposeRepository)
        {
            _tagRepository = tagRepository;
            _newsRepository = newsRepository;
            _commentRepository = commentRepository;
            _financialPurposeRepository = financialPurposeRepository;
        }

        public Project ConvertTo(ProjectSearchNote item)
        {
            throw new System.NotImplementedException();
        }

        public ProjectSearchNote ConvertFrom(Project item)
        {
            var result = new ProjectSearchNote
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description
            };
            AddOtherFields(result);
            return result;
        }

        private void AddOtherFields(ProjectSearchNote item)
        {
            AddTags(item);
            AddNews(item);
            AddComments(item);
            AddFinancialPurposes(item);
        }

        private void AddFinancialPurposes(ProjectSearchNote item)
        {
            var purposes = _financialPurposeRepository.GetWhere(n => n.ProjectId == item.Id);
            item.FinancialPurposeName = purposes.Select(n => n.Name).ToArray();
            item.FinancialPurposeDescription = purposes.Select(n => n.Description).ToArray();
        }

        private void AddTags(ProjectSearchNote item)
        {
            var tags = _tagRepository.GetWhere(n => n.ProjectId == item.Id);
            item.Tag = tags.Select(n => n.Name).ToArray();
        }

        private void AddNews(ProjectSearchNote item)
        {
            var news = _newsRepository.GetWhere(n => n.ProjectId == item.Id);
            item.NewsSubject = news.Select(n => n.Subject).ToArray();
            item.NewsText = news.Select(n => n.Text).ToArray();
        }

        private void AddComments(ProjectSearchNote item)
        {
            var comments = _commentRepository.GetWhere(n => n.ProjectId == item.Id);
            item.Comment = comments.Select(n => n.Text).ToArray();
        }
    }
}
