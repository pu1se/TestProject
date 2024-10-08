using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserMonitoringAndHistory;
using UserMonitoringAndHistory.Data;
using Microsoft.AspNetCore.Identity;

namespace Tests.UserMonitoringAndHistory
{
    public abstract class BaseServiceTests<TService>
    {
        private static bool _isFirstCall = true;
        private IServiceScope _scope;
        private static ServiceProvider _serviceProvider;
        private static readonly object _lock = new object();
        protected TService Service { get; private set; }
        protected ApplicationDbContext DB { get; private set; }

        [TestInitialize]
        public async Task BaseInitialize()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

            // Ensure this block runs only once across all tests
            if (_isFirstCall)
            {
                lock (_lock)
                {
                    if (_isFirstCall)  // Double-check in case multiple threads reach here simultaneously
                    {
                        _serviceProvider = GetDiContainer();

                        _isFirstCall = false;
                    }
                }
            }

            // Create a new scope for the current test
            _scope = _serviceProvider.CreateScope();   
            Service = Resolve<TService>();
            DB = Resolve<ApplicationDbContext>();
        }

        [TestCleanup]
        public void BaseCleanup()
        {
            DB?.Dispose();
            _scope?.Dispose();
        }

        protected T Resolve<T>()
        {
            return _scope.ServiceProvider.GetRequiredService<T>();
        }

        private ServiceProvider GetDiContainer()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = configurationBuilder.Build();
            var services = new ServiceCollection();
            DependencyManager.RegisterDependencies(services, configuration);
            return services.BuildServiceProvider();
        }
    }
}
