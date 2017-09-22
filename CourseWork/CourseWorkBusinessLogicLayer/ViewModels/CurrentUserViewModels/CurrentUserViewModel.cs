namespace CourseWork.BusinessLogicLayer.ViewModels.CurrentUserViewModels
{
    public class CurrentUserViewModel
    {
        public string UserName { get; set; }

        public string Avatar { get; set; }

        public string Role { get; set; }

		public bool IsBlocked { get; set; }
    }
}
