using System.Collections.Generic;
using System.Linq;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using CourseWork.DataLayer.Repositories.Implementations;

namespace CourseWork.BusinessLogicLayer.Services.AdminManagers
{
    public class AdminManager : IAdminManager
    {
        private readonly Repository<UserInfo> _userInfoRepository;
        private readonly IMapper<UserListItemViewModel, UserInfo> _mapper;

        public AdminManager(IMapper<UserListItemViewModel, UserInfo> mapper, Repository<UserInfo> userInfoRepository, ApplicationDbContext context, Repository<ApplicationUser> applicationUserRepository)
        {
            _mapper = mapper;
            _userInfoRepository = userInfoRepository;
        }

        public UserListItemViewModel[] GetAllUsers()
        {
            return _userInfoRepository.GetAll().Select(n => _mapper.ConvertFrom(n)).ToArray();
        }
    }
}
