using System;
using System.Threading.Tasks;
using UserMonitoringAndHistory.Services.User.Handlers.RefreshLoginInfo;

namespace UserMonitoringAndHistory.Services.User
{
    public class UserService : BaseService
    {
        public UserService(IServiceProvider services) : base(services)
        {
        }

        public Task<CallResult> RefreshLoginInfo(string userEmail)
        {
            var handler = GetHandler<RefreshLoginInfoCommandHandler>();
            return handler.HandleAsync(new RefreshLoginInfoCommand
            {
                UserEmail = userEmail
            });
        }
    }
}
