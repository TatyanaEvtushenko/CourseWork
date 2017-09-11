using System;

namespace CourseWork.DataLayer.Models
{
    public class Payment
    {
        public string Id { get; set; }

        public string ProjectId { get; set; }

        public string UserName { get; set; }

        public DateTime Time { get; set; }

        public decimal PaidAmount { get; set; }
    }
}
