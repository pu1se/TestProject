using UserMonitoringAndHistory._Core.CallResults;

namespace UserMonitoringAndHistory.Services.User.Handlers.GetUserList
{
    public class GetUserListQuery : Query
    {
        [NotDefaultAndNotNullValueRequired]
        public string OnBehalfOfUserId { get; init; }
    }
}
