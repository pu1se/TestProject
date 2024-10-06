using Microsoft.AspNetCore.Identity;
using System.Linq;
using System;
using UserMonitoringAndHistory.Models;

namespace UserMonitoringAndHistory.Data
{
    public static class TestData
    {
        public static readonly string AdminUserEmail = "admin@admin.com";
        public static readonly string AdminUserName = "admin@admin.com";
        public static readonly string AdminUserPassword = "admin@admin.com";
        public static readonly Guid AdminUserId = new Guid("1118aa58-c48e-4696-98cf-1b6e22c63076");
    }

    public static class DatabaseInitializer
    {
        public static void BaseSeeding(
            ApplicationDbContext applicationDbContext,
            UserManager<ApplicationUser> userManager
        )
        {
            var dbWasInitialized = applicationDbContext.Roles.Any();
            if (dbWasInitialized)
            {
                return;
            }

            applicationDbContext.Roles.Add(new IdentityRole
            {
                Id = UserRoleType.Standard.ToString(),
                Name = UserRoleType.Standard.ToString(),
                ConcurrencyStamp = DateTime.UtcNow.ToLongTimeString()
            });
            applicationDbContext.Roles.Add(new IdentityRole
            {
                Id = UserRoleType.Admin.ToString(),
                Name = UserRoleType.Admin.ToString(),
                ConcurrencyStamp = DateTime.UtcNow.ToLongTimeString()
            });

            var adminUser = new ApplicationUser
            {
                Id = TestData.AdminUserId.ToString(),
                UserName = TestData.AdminUserName,
                Email = TestData.AdminUserEmail,
                ConcurrencyStamp = DateTime.UtcNow.ToLongTimeString(),
                EmailConfirmed = true,
            };

            var result = userManager.CreateAsync(adminUser, TestData.AdminUserPassword).GetAwaiter().GetResult();

            if (result.Succeeded)
            {
                applicationDbContext.SaveChanges();

                applicationDbContext.UserRoles.Add(new IdentityUserRole<string>
                {
                    RoleId = UserRoleType.Admin.ToString(),
                    UserId = TestData.AdminUserId.ToString()
                });
                applicationDbContext.SaveChanges();
            }
        }
    }
}
