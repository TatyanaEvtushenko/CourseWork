using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.DataLayer.Enums;
using CourseWork.DataLayer.Enums.Configurations;
using CourseWork.DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CourseWork.Extensions.StartupExtensions
{
    public static class DatabaseRolesExtension
    {
        public static async Task CreateDatabaseRoles(this IServiceCollection services, List<string> adminUserNames)
        {
            var serviceProvider = services.BuildServiceProvider();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            foreach (var role in EnumConfiguration.RoleNames)
            {
                await CreateRole(roleManager, role.Value);
            }
            await SetAdmins(userManager, adminUserNames);
        }

        private static async Task CreateRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        private static async Task SetAdmins(UserManager<ApplicationUser> userManager, List<string> adminUserNames)
        {
            foreach (var admin in adminUserNames)
            {
                var user = await userManager.FindByNameAsync(admin);
                if (user != null)
                {
                    await ChangeRole(userManager, user);
                }
            }
        }

        private static async Task ChangeRole(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            if (user.Roles.Count > 0)
            {
                var role = user.Roles.ElementAt(0);
                await userManager.RemoveFromRoleAsync(user, role.RoleId);
            }
            await userManager.AddToRoleAsync(user, EnumConfiguration.RoleNames[UserRole.Admin]);
        }
    }
}
