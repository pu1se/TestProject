using UserMonitoringAndHistory._Core.CallResults;

namespace UserMonitoringAndHistory.Services.User.Handlers.SetToUserAdminRole
{
    public class SetToUserAdminRoleCommand : Command
    {
        [NotDefaultAndNotNullValueRequired]
        public string UserId { get; set; }
    }
}
