using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserMonitoringAndHistory.Data;
using UserMonitoringAndHistory.Data.Enums;

namespace UserMonitoringAndHistory.Services.User.Handlers.SetToUserAdminRole
{
    public class SetToUserAdminRoleCommandHandler : CommandHandler<SetToUserAdminRoleCommand, CallResult>
    {
        public SetToUserAdminRoleCommandHandler(ApplicationDbContext db) : base(db)
        {
        }

        protected override async Task<CallResult> HandleCommandAsync(SetToUserAdminRoleCommand command)
        {
            var isUserAlreadyHasAdminRole = await DB.UserRoles
                .AnyAsync(
                    el => 
                        el.UserId == command.UserId 
                        &&
                        el.RoleId == UserRoleType.Admin.ToString());
            if (isUserAlreadyHasAdminRole)
            {
                return SuccessResult();
            }

            var isUserExists = await DB.Users.AnyAsync(el => el.Id == command.UserId);
            if (isUserExists == false)
            {
                return NotFoundResult("User not found.");
            }


            DB.UserRoles.Add(new IdentityUserRole<string>
            {
                RoleId = UserRoleType.Admin.ToString(),
                UserId = command.UserId
            });
            await DB.SaveChangesAsync();

            return SuccessResult();
        }
    }
}
