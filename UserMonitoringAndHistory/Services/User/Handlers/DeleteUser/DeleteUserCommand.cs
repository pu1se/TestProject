using UserMonitoringAndHistory._Core.CallResults;

namespace UserMonitoringAndHistory.Services.User.Handlers.DeleteUser
{
    public class DeleteUserCommand : Command
    {
        [NotDefaultAndNotNullValueRequired]
        public string OnBehalfOfUserId { get; init; }

        [NotDefaultAndNotNullValueRequired]
        public string UserId { get; init; }
    }
}
