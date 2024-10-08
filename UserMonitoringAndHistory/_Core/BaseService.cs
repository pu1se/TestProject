using System;
using Microsoft.Extensions.DependencyInjection;
using UserMonitoringAndHistory._Core.Handler;

namespace UserMonitoringAndHistory
{
    public abstract class BaseService
    {
        private readonly IServiceProvider _services;

        protected BaseService(IServiceProvider services)
        {
            _services = services;
        }

        protected THandler GetHandler<THandler>() where THandler : IHandler
        {
            return _services.GetRequiredService<THandler>();
        }
    }
}
