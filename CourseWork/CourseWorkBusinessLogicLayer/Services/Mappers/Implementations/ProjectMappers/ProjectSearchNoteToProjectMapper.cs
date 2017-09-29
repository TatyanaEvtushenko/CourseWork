using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.ElasticSearch.Documents;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.ProjectMappers
{
    public class ProjectSearchNoteToProjectMapper : IMapper<ProjectSearchNote, Project>
    {
        public Project ConvertTo(ProjectSearchNote item)
        {
            throw new System.NotImplementedException();
        }

        public ProjectSearchNote ConvertFrom(Project item)
        {
            var result = CreateProjectSearchNote(item);
            AddOtherFields(result, item);
            return result;
        }

        private void AddOtherFields(ProjectSearchNote note, Project project)
        {
            AddTags(note, project.Tags);
            AddNews(note, project.News);
            AddComments(note, project.Comments);
            AddFinancialPurposes(note, project.FinancialPurposes);
        }

        private ProjectSearchNote CreateProjectSearchNote(Project project)
        {
            return new ProjectSearchNote
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description
            };
        }

        private void AddFinancialPurposes(ProjectSearchNote note, IEnumerable<FinancialPurpose> purposes)
        {
            note.FinancialPurposeName = purposes?.Select(n => n.Name).ToList();
            note.FinancialPurposeDescription = purposes?.Select(n => n.Description).ToList();
        }

        private void AddTags(ProjectSearchNote item, IEnumerable<Tag> tags)
        {
            item.Tag = tags?.Select(n => n.Name).ToList();
        }

        private void AddNews(ProjectSearchNote item, IEnumerable<News> news)
        {
            item.NewsSubject = news?.Select(n => n.Subject).ToList();
            item.NewsText = news?.Select(n => n.Text).ToList();
        }

        private void AddComments(ProjectSearchNote item, IEnumerable<Comment> comments)
        {
            item.Comment = comments?.Select(n => n.Text).ToList();
        }
    }
}
