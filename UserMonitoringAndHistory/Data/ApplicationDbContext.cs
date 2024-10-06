using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMonitoringAndHistory.Models;

namespace UserMonitoringAndHistory.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
         private static bool MigrationWasChecked { get; set; } = false;

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            IServiceProvider serviceProvider) : base(options, operationalStoreOptions)
        {
            if (!MigrationWasChecked)
            {
                MigrationWasChecked = true;
                try
                {
                    Database.Migrate();
                    var userManager = serviceProvider.GetService(typeof(UserManager<ApplicationUser>)) as UserManager<ApplicationUser>;
                    DatabaseInitializer.BaseSeeding(this, userManager);
                }
                catch(Exception ex)
                {
                }
            }
        }
    }
}
