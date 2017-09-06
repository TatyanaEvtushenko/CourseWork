using Microsoft.AspNetCore.Http;

namespace CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels
{
    public class UserConfirmationViewModel
    {
        public string PassportScan { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Description { get; set; }
    }
}
