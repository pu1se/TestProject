using System;
using System.Threading.Tasks;
using UserMonitoringAndHistory.Services.User.Handlers.DeleteUser;
using UserMonitoringAndHistory.Services.User.Handlers.GetUserList;
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

        public Task<CallListResult<GetUserListItemResult>> GetUserList()
        {
            var handler = GetHandler<GetUserListQueryHandler>();
            return handler.HandleAsync(EmptyQuery.Value);
        }

        public Task<CallResult> DeleteUser(string userId)
        {
            var handler = GetHandler<DeleteUserCommandHandler>();
            return handler.HandleAsync(
                new DeleteUserCommand
                {
                    UserId = userId
                });
        }
    }
}
