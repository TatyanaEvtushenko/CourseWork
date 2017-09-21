using CourseWork.DataLayer.Enums;

namespace CourseWork.DataLayer.Models
{
    public class Award
    {
        public string UserName { get; set; }

        public AwardType AwardType { get; set; }

        public byte Level { get; set; }

        public UserInfo UserInfo { get; set; }
    }
}
