﻿using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.AwardManagers
{
    public interface IAwardManager
    {
        bool AddAwardForComments();

        bool AddAwardForReceivedSubscriptions(string projectId);

        bool AddAwardForProjects();

        bool AddAwardForPayments(Payment payment);

        bool AddAwardForReceivedPayments(Project project);

        decimal GetNeccessaryCountForAward(AwardType type, int level);

        int GetTrueLevelNumber(int level);
    }
}
