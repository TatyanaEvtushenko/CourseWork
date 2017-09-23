namespace CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels
{
    public class UserListItemViewModel
    {
        public string UserName { get; set; }

        public string Status { get; set; }

        public string RegistrationTime { get; set; }

        public string LastLoginTime { get; set; }

        public string ProjectNumber { get; set; }

        public string Raiting { get; set; }

        public int StatusCode { get; set; }

        public bool IsBlocked { get; set; }

        public string Avatar;
    }
}
