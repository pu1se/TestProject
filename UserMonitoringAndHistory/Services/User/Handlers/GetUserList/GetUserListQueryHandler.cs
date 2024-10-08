using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserMonitoringAndHistory.Data;

namespace UserMonitoringAndHistory.Services.User.Handlers.GetUserList
{
    public class GetUserListQueryHandler : QueryHandler<EmptyQuery, CallListResult<GetUserListItemResult>>
    {
        public GetUserListQueryHandler(ApplicationDbContext db) : base(db)
        {
        }

        protected override async Task<CallListResult<GetUserListItemResult>> HandleQueryAsync(EmptyQuery query)
        {
            var users = await DB.Users.Select(el => new GetUserListItemResult
            {
                UserId = el.Id,
                Name = el.UserName,
                Email = el.Email,
            })
            .ToListAsync();

            return SuccessListResult(users);
        }
    }
}
