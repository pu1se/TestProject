using System;
using System.Threading.Tasks;
using UserMonitoringAndHistory.Services.User.Handlers.DeleteUser;
using UserMonitoringAndHistory.Services.User.Handlers.GetUserList;
using UserMonitoringAndHistory.Services.User.Handlers.RefreshLoginInfo;
using UserMonitoringAndHistory.Services.User.Handlers.SetToUserAdminRole;

namespace UserMonitoringAndHistory.Services.User
{
    public class UserService : BaseService
    {
        public UserService(IServiceProvider services) : base(services)
        {
        }

        public Task<CallResult> RefreshLoginInfo(RefreshLoginInfoCommand command)
        {
            var handler = GetHandler<RefreshLoginInfoCommandHandler>();
            return handler.HandleAsync(command);
        }

        public Task<CallListResult<GetUserListItemResult>> GetUserList(GetUserListQuery query)
        {
            var handler = GetHandler<GetUserListQueryHandler>();
            return handler.HandleAsync(query);
        }

        public Task<CallResult> DeleteUser(DeleteUserCommand command)
        {
            var handler = GetHandler<DeleteUserCommandHandler>();
            return handler.HandleAsync(command);
        }

        public Task<CallResult> SetToUserAdminRole(SetToUserAdminRoleCommand command)
        {
            var handler = GetHandler<SetToUserAdminRoleCommandHandler>();
            return handler.HandleAsync(command);
        }
    }
}
