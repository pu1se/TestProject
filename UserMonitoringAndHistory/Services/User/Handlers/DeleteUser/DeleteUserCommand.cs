namespace UserMonitoringAndHistory.Services.User.Handlers.DeleteUser
{
    public class DeleteUserCommand : Command
    {
        public string UserId { get; set; }
    }
}
