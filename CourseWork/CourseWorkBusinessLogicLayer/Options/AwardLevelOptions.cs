using System.Collections.Generic;

namespace CourseWork.BusinessLogicLayer.Options
{
    public class AwardLevelOptions
    {
        public byte FirstLevel = 1;

        public Dictionary<int, int> CommentLevels => new Dictionary<int, int>
        {
            {FirstLevel,     1},
            {FirstLevel + 1, 5},
            {FirstLevel + 2, 10},
            {FirstLevel + 3, 30},
            {FirstLevel + 4, 50},
            {FirstLevel + 5, 80},
            {FirstLevel + 6, 100},
            {FirstLevel + 7, 200},
            {FirstLevel + 8, 500},
        };

        public Dictionary<int, int> PaymentLevels => new Dictionary<int, int>
        {
            {FirstLevel,     1},
            {FirstLevel + 1, 10},
            {FirstLevel + 2, 20},
            {FirstLevel + 3, 50},
            {FirstLevel + 4, 100},
            {FirstLevel + 5, 200},
            {FirstLevel + 6, 300},
            {FirstLevel + 7, 500},
            {FirstLevel + 8, 1000},
        };

        public Dictionary<int, int> ProjectLevels => new Dictionary<int, int>
        {
            {FirstLevel,     1},
            {FirstLevel + 1, 3},
            {FirstLevel + 2, 5},
            {FirstLevel + 3, 10},
            {FirstLevel + 4, 15},
            {FirstLevel + 5, 20},
            {FirstLevel + 6, 30},
            {FirstLevel + 7, 50},
            {FirstLevel + 8, 100},
        };

        public Dictionary<int, int> SubscriptionLevels => new Dictionary<int, int>
        {
            {FirstLevel,     1},
            {FirstLevel + 1, 5},
            {FirstLevel + 2, 10},
            {FirstLevel + 3, 20},
            {FirstLevel + 4, 30},
            {FirstLevel + 5, 50},
            {FirstLevel + 6, 80},
            {FirstLevel + 7, 100},
            {FirstLevel + 8, 150},
        };

        public Dictionary<int, int> ReceivedPaymentLevels => new Dictionary<int, int>
        {
            {FirstLevel,     1},
            {FirstLevel + 1, 5},
            {FirstLevel + 2, 10},
            {FirstLevel + 3, 30},
            {FirstLevel + 4, 50},
            {FirstLevel + 5, 80},
            {FirstLevel + 6, 100},
            {FirstLevel + 7, 200},
            {FirstLevel + 8, 500},
        };
    }
}
