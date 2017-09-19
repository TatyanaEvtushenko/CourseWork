using System.Collections.Generic;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.ViewModels.NewsViewModels;

namespace CourseWork.BusinessLogicLayer.Services.NewsManagers
{
    public interface INewsManager
    {
        bool AddNews(NewsFormViewModel newsForm);

        Task<bool> AddMailingToSubscribers(NewsFormViewModel newsForm);

        Task<bool> AddMailingToPayers(NewsFormViewModel newsForm);

        bool RemoveNews(string newsId);

        IEnumerable<NewsViewModel> GetLastNews();
    }
}
