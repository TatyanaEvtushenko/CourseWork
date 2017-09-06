using System.Threading.Tasks;
using CourseWork.DataLayer.Enums.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CourseWork.Extensions.StartupExtensions
{
    public static class DatabaseRolesExtension
    {
        public static async Task CreateDatabaseRoles(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            foreach (var role in EnumConfiguration.RoleNames)
            {
                await CreateRole(roleManager, role.Value);
            }
        }

        private static async Task CreateRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
