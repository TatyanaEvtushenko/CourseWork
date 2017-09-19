namespace CourseWork.DataLayer.Models
{
    public class Message
    {
        public string Id { get; set; }

        public string RecipientUserName { get; set; }

        public string Text { get; set; }

        public bool IsSeen { get; set; }
    }
}
