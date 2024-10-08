using System.Threading.Tasks;
using UserMonitoringAndHistory.Data;

namespace UserMonitoringAndHistory.Services.User.Handlers.DeleteUser
{
    public class DeleteUserCommandHandler : CommandHandler<DeleteUserCommand, CallResult>
    {
        public DeleteUserCommandHandler(ApplicationDbContext db) : base(db)
        {
        }

        protected override Task<CallResult> HandleCommandAsync(DeleteUserCommand command)
        {
            return Task.FromResult(new CallResult());
        }
    }
}
