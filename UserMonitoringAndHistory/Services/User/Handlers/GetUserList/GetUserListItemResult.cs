using System;

namespace UserMonitoringAndHistory.Services.User.Handlers.GetUserList
{
    public class GetUserListItemResult
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public byte[] ProfileImage { get; set; }
        public DateTime? LastLoginDateUtc { get; set; }
        public int CountLoginNumber { get; set; }
    }
}
