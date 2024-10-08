using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserMonitoringAndHistory.Data;
using UserMonitoringAndHistory.Data.Enums;

namespace UserMonitoringAndHistory.Services.User.Handlers.DeleteUser
{
    public class DeleteUserCommandHandler : CommandHandler<DeleteUserCommand, CallResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteUserCommandHandler(ApplicationDbContext db, UserManager<ApplicationUser> userManager) : base(db)
        {
            _userManager = userManager;
        }

        protected override async Task<CallResult> HandleCommandAsync(DeleteUserCommand command)
        {
            if (command.UserId == command.OnBehalfOfUserId)
            {
                return ValidationFailResult(nameof(command.OnBehalfOfUserId), "You can't delete yourself.");
            }

            var userRole = await DB.UserRoles
                .FirstOrDefaultAsync(
                    el => 
                        el.UserId == command.OnBehalfOfUserId 
                        &&
                        el.RoleId == UserRoleType.Admin.ToString());
            if (userRole == null)
            {
                return ValidationFailResult(nameof(command.OnBehalfOfUserId), "Only admin can delete users.");
            }

            var userForDeleting = await _userManager.FindByIdAsync(command.UserId);
            if (userForDeleting == null)
            {
                return NotFoundResult("User not found.");
            }

            await _userManager.DeleteAsync(userForDeleting);

            return SuccessResult();
        }
    }
}
