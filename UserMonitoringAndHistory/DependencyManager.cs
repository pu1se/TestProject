using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserMonitoringAndHistory.Data;

namespace UserMonitoringAndHistory
{
    public static class DependencyManager
    {
        public static void RegisterDependencies(IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = new AppSettings(configuration);
            services.AddSingleton<AppSettings>(_ => appSettings);
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(appSettings.DatabaseConnection));
        }
    }
}
