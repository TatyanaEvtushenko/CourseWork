namespace CourseWork.BusinessLogicLayer.ViewModels.FinancialPurposeViewModels
{
    public class FinancialPurposeViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Budget { get; set; }

        public bool IsReached { get; set; }
    }
}
