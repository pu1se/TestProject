using System;
using Microsoft.Extensions.Configuration;

namespace UserMonitoringAndHistory
{
    public class AppSettings
    {
        public AppSettings(IConfiguration? configuration)
        {
            if (configuration == null)
            {
                return;
            }
            
            Environment = configuration["Environment"] ?? throw new InvalidOperationException();
            DatabaseConnection = configuration["DatabaseConnection"] ?? throw new InvalidOperationException();
        }

        public string DatabaseConnection { get; }
        public string Environment { get; }
    }
}
