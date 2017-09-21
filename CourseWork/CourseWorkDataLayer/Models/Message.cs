using System.ComponentModel.DataAnnotations;

namespace CourseWork.DataLayer.Models
{
    public class Message
    {
        [Key]
        public string Id { get; set; }

        public string RecipientUserName { get; set; }

        public string Text { get; set; }

        public bool IsSeen { get; set; }

        public UserInfo RecipientInfo { get; set; }
    }
}
