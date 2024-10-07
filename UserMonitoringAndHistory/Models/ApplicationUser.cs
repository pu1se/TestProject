using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMonitoringAndHistory.Models
{
    public class ApplicationUser : IdentityUser
    {
        public byte[] ProfileImage { get; set; }

        public DateTime? LastLoginDateUtc { get; set; }

        public int CountLoginNumber { get; set; }
    }
}
