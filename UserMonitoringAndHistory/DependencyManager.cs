using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserMonitoringAndHistory._Core.Handler;
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

            services.AddTransientHandlers();
            services.AddTransientServices();
        }

        private static IEnumerable<Type> _allTypes;
        private static IEnumerable<Type> AllTypes
        {
            get { return _allTypes ??= Assembly.GetExecutingAssembly().GetTypes(); }
        }

        private static IEnumerable<Type> _handlerTypes;
        private static void AddTransientHandlers(this IServiceCollection services)
        {
            _handlerTypes ??= AllTypes
                .Where(
                    type =>
                        type.GetInterfaces().Contains(typeof(IHandler)) &&
                        type.BaseType != typeof(CallResultShortcuts)
                );

            foreach (var type in _handlerTypes)
            {
                services.AddTransient(type);
            }
        }

        private static IEnumerable<Type> _serviceTypes;
        private static void AddTransientServices(this IServiceCollection services)
        {
            _serviceTypes ??= AllTypes.Where(type => type.BaseType == typeof(BaseService));

            foreach (var type in _serviceTypes)
            {
                services.AddTransient(type);
            }
        }
    }
}
