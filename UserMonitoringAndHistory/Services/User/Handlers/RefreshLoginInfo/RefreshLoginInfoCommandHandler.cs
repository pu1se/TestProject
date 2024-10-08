using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserMonitoringAndHistory.Data;

namespace UserMonitoringAndHistory.Services.User.Handlers.RefreshLoginInfo
{
    public class RefreshLoginInfoCommandHandler : CommandHandler<RefreshLoginInfoCommand, CallResult>
    {
        public RefreshLoginInfoCommandHandler(ApplicationDbContext db) : base(db)
        {
        }

        protected override async Task<CallResult> HandleCommandAsync(RefreshLoginInfoCommand command)
        {
            var user = await DB.Users.FirstOrDefaultAsync(el => el.Email == command.UserEmail);
            if (user == null)
            {
                return NotFoundResult($"User with id {command.UserEmail} was not found.");
            }

            user.LastLoginDateUtc = DateTime.UtcNow;
            user.CountLoginNumber++;

            await DB.SaveChangesAsync();

            return SuccessResult();
        }
    }
}
