using Microsoft.AspNetCore.Identity;
using System;

namespace UserMonitoringAndHistory
{
    public class ApplicationUser : IdentityUser
    {
        public byte[] ProfileImage { get; set; }

        public DateTime? LastLoginDateUtc { get; set; }

        public int CountLoginNumber { get; set; }
    }
}
