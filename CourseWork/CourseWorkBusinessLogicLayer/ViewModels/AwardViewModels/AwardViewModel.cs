using CourseWork.DataLayer.Enums;

namespace CourseWork.BusinessLogicLayer.ViewModels.AwardViewModels
{
    public class AwardViewModel
    {
        public decimal NeccessaryCount { get; set; }

        public AwardType Type { get; set; }

        public int Level { get; set; }
    }
}
