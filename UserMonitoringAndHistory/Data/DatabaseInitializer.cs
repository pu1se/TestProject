using Microsoft.AspNetCore.Identity;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using UserMonitoringAndHistory.Data.Enums;

namespace UserMonitoringAndHistory.Data
{
    public static class TestData
    {
        public static readonly string AdminUserEmail = "admin@admin.com";
        public static readonly string AdminUserName = "admin@admin.com";
        public static readonly string AdminUserPassword = "admin@admin.com";
        public static readonly string AdminUserId = "1118aa58-c48e-4696-98cf-1b6e22c63076";
    }

    public static class DatabaseInitializer
    {
        public static void Seed(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager
        )
        {
            dbContext.Database.Migrate();

            var dbWasInitialized = dbContext.Roles.Any();
            if (dbWasInitialized)
            {
                return;
            }

            dbContext.Roles.Add(new IdentityRole
            {
                Id = UserRoleType.Standard.ToString(),
                Name = UserRoleType.Standard.ToString(),
                ConcurrencyStamp = DateTime.UtcNow.ToLongTimeString()
            });
            dbContext.Roles.Add(new IdentityRole
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
                dbContext.SaveChanges();

                dbContext.UserRoles.Add(new IdentityUserRole<string>
                {
                    RoleId = UserRoleType.Admin.ToString(),
                    UserId = TestData.AdminUserId
                });
                dbContext.SaveChanges();
            }
        }
    }
}
