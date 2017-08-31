using System;

namespace CourseWork.DataLayer.Models
{
    public class Comment
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string ProjectId { get; set; }

        public DateTime Time { get; set; }

        public string Text { get; set; }
    }
}
