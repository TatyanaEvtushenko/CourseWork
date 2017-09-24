using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.AwardManagers
{
    public interface IAwardManager
    {
        bool AddAwardForComments(string awardName);

        bool AddAwardForReceivedSubscriptions(string projectId, string awardName);

        bool AddAwardForProjects(string awardName);

        bool AddAwardForPayments(Payment payment, string awardName);

        bool AddAwardForReceivedPayments(Project project, string awardName);

        decimal GetNeccessaryCountForAward(AwardType type, int level);

        int GetTrueLevelNumber(int level);
    }
}
