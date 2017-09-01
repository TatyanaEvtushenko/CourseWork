namespace CourseWork.DataLayer.Models
{
    public class FinancialPurposeInProject
    {
        public string Id { get; set; }

        public string FinancialPurposeId { get; set; }

        public string ProjectId { get; set; }

        public decimal PaidAmount { get; set; }
    }
}
