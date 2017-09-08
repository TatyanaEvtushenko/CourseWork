﻿using System;

namespace CourseWork.DataLayer.Models
{
    public class Comment
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string ProjectId { get; set; }

        public DateTime Time { get; set; }

        public string Text { get; set; }
    }
}
