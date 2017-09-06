using System.ComponentModel.DataAnnotations;

namespace CourseWork.DataLayer.Models
{
    public class FinancialPurpose
    {
        [Key]
        public string Id { get; set; }

        public string ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal NecessaryPaymentAmount { get; set; }

        public bool IsReached { get; set; }
    }
}
