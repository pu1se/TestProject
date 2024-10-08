using UserMonitoringAndHistory._Core.CallResults;

namespace UserMonitoringAndHistory.Services.User.Handlers.RefreshLoginInfo
{
    public class RefreshLoginInfoCommand : Command
    {
        [NotDefaultAndNotNullValueRequired]
        public string UserEmail { get; init; }
    }
}
