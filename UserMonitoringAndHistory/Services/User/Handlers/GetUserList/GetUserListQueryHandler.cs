using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserMonitoringAndHistory.Data;
using UserMonitoringAndHistory.Data.Enums;

namespace UserMonitoringAndHistory.Services.User.Handlers.GetUserList
{
    public class GetUserListQueryHandler : QueryHandler<GetUserListQuery, CallListResult<GetUserListItemResult>>
    {
        public GetUserListQueryHandler(ApplicationDbContext db) : base(db)
        {
        }

        protected override async Task<CallListResult<GetUserListItemResult>> HandleQueryAsync(GetUserListQuery query)
        {
            var userRoles = await DB.UserRoles.ToListAsync();
            var IsCurrentUserInRoleAdmin = userRoles.Any(x => x.UserId == query.OnBehalfOfUserId && x.RoleId == UserRoleType.Admin.ToString());

            var users = await DB.Users.Select(el => new GetUserListItemResult
            {
                UserId = el.Id,
                Name = el.UserName,
                Email = el.Email,
                ProfileImage = el.ProfileImage,
                LastLoginDateUtc = el.LastLoginDateUtc,
                CountLoginNumber = el.CountLoginNumber,
                ThisIsCurrentUser = query.OnBehalfOfUserId == el.Id,
                IsCurrentUserInRoleAdmin = IsCurrentUserInRoleAdmin
            })
            .ToListAsync();

            

            foreach (var user in users)
            {
                user.IsAdmin = userRoles.Any(x => x.UserId == user.UserId && x.RoleId == UserRoleType.Admin.ToString());
            }

            return SuccessListResult(users);
        }
    }
}
