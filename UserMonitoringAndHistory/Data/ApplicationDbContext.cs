using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
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
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
            if (!MigrationWasChecked)
            {
                MigrationWasChecked = true;
                try
                {
                    Database.Migrate();
                    //DatabaseInitializer.BaseSeeding(this);
                }
                catch(Exception ex)
                {
                }
            }
        }
    }
}
