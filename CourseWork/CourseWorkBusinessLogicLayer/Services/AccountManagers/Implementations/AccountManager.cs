﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.Services.MessageSenders;
using CourseWork.BusinessLogicLayer.ViewModels.UserInfoViewModels;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Enums.Configurations;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CourseWork.BusinessLogicLayer.Services.AccountManagers.Implementations
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRepository<UserInfo> _userInfoRepository;
        private readonly IRepository<Rating> _ratingRepository;
        private readonly IMapper<DisplayableInfoViewModel, UserInfo> _mapper;

        public AccountManager(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IHttpContextAccessor contextAccessor, IRepository<UserInfo> userInfoRepository,
            IMapper<DisplayableInfoViewModel, UserInfo> mapper, IRepository<Rating> ratingRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _contextAccessor = contextAccessor;
            _userInfoRepository = userInfoRepository;
            _mapper = mapper;
            _ratingRepository = ratingRepository;
        }

        public async Task<bool> Register(string userName, string email, string password, string messageSubject, string messagePrototype)
        {
            var user = new ApplicationUser {UserName = userName ?? email, Email = email};
            return await TryRegister(user, password, messageSubject, messagePrototype);
        }

        public async Task<bool> ConfirmRegistration(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return false;
            }
            var user = await _userManager.FindByIdAsync(userId);
            return user != null && await TryConfirmRegistration(user, code);
        }

        public async Task<bool> Login(string email, string password)
        {
            var user = _userInfoRepository.FirstOrDefault(u => u.ApplicationUser.Email == email, u => u.ApplicationUser);
            if (user == null || !user.ApplicationUser.EmailConfirmed || user.IsBlocked)
            {
                return false;
            }
            return await TryLogin(user, password);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task AddRole(string userName, UserRole role)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await AddRole(user, role);
        }

        public async Task RemoveRole(string userName, UserRole role)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await RemoveRole(user, role);
        }

        public DisplayableInfoViewModel[] GetDisplayableInfo(string[] userNames)
        {
            var ratings = _ratingRepository.GetWhere(r => userNames.Contains(r.Project.OwnerUserName), 
                r => r.Project);
            var infos = _userInfoRepository.GetWhere(item => userNames.Contains(item.UserName),
                item => item.Projects, item => item.Awards);
            return infos.Select(item => PrepareDisplayableInfo(item, ratings)).ToArray();
        }

        public DisplayableInfoViewModel GetUserDisplayableInfo(string username)
        {
            return GetDisplayableInfo(new[] {username}).SingleOrDefault();
        }

        private DisplayableInfoViewModel PrepareDisplayableInfo(UserInfo info, IEnumerable<Rating> ratings)
        {
            var viewModel = _mapper.ConvertFrom(info);
            var userRatings = ratings.Where(r => r.Project.OwnerUserName == info.UserName);
            viewModel.Rating = !userRatings.Any() ? 0 : userRatings.Average(r => r.RatingResult);
            return viewModel;
        }

        private async Task<bool> TryLogin(UserInfo user, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, true, false);
            if (result.Succeeded)
            {
                UpdateLoginTime(user);
            }
            return result.Succeeded;
        }

        private async Task<bool> TryRegister(ApplicationUser user, string password, string messageSubject, string messagePrototype)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return false;
            }
            await SendConfirmation(user, messageSubject, messagePrototype);
            return true;
        }

        private async Task<bool> TryConfirmRegistration(ApplicationUser user, string code)
        {
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return result.Succeeded && await AddNewUser(user);
        }

        private async Task<bool> AddNewUser(ApplicationUser user)
        {
            await AddRole(user, UserRole.User);
            var userInfo = CreateBasicUserInfo(user.UserName);
            return _userInfoRepository.AddRange(userInfo);
        }

        private UserInfo CreateBasicUserInfo(string userName)
        {
            return new UserInfo
            {
                UserName = userName,
                IsBlocked = false,
                Status = UserStatus.WithoutConfirmation,
                LastLoginTime = DateTime.UtcNow,
                RegistrationTime = DateTime.UtcNow,
            };
        }

        private void UpdateLoginTime(UserInfo user)
        {
            user.LastLoginTime = DateTime.UtcNow;
            _userInfoRepository.UpdateRange(user);
        }

        private async Task SendConfirmation(ApplicationUser user, string messageSubject, string messagePrototype)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = GetCallBackConfirmUrl(user.Id, code);
            await _emailSender.SendEmailAsync(user.Email, messageSubject, GetMessageToSendConfirmLink(callbackUrl, messagePrototype));
        }

        private string GetCallBackConfirmUrl(string userId, string code) =>
            $"{GetBaseUrl()}/api/Account/ConfirmRegistration?userId={userId}&code={System.Net.WebUtility.UrlEncode(code)}";

        private string GetBaseUrl() =>
            $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}";

        private async Task AddRole(ApplicationUser user, UserRole role)
        {
            await _userManager.AddToRoleAsync(user, EnumConfiguration.RoleNames[role]);
        }

        private async Task RemoveRole(ApplicationUser user, UserRole role)
        {
            await _userManager.RemoveFromRoleAsync(user, EnumConfiguration.RoleNames[role]);
        }

        private static string GetMessageToSendConfirmLink(string url, string messagePrototype) =>
            string.Format(messagePrototype, url);
    }
}
