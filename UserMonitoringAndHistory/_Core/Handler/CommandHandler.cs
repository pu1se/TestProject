using System.Threading.Tasks;
using System;
using UserMonitoringAndHistory._Core.CallResults;
using UserMonitoringAndHistory._Core.Handler;
using UserMonitoringAndHistory.Data;

namespace UserMonitoringAndHistory
{
    public abstract class CommandHandler<TCommand, TResult> : CallResultShortcuts, IHandler 
        where TResult : CallResult, new() 
        where TCommand : Command
    {
        protected ApplicationDbContext DB { get; }

        protected CommandHandler(ApplicationDbContext db)
        {
            DB = db;
        }

        protected abstract Task<TResult> HandleCommandAsync(TCommand command);

        private TResult ValidateModel(TCommand command)
        {
            if (!command.IsValid())
            {
                var result = new TResult();
                var validationResult = Result.ValidationFail(command);
                result.SetResult(validationResult);
                return result;
            }

            return new TResult();
        }

        public async Task<TResult> HandleAsync(TCommand command)
        {
            var validationResult = ValidateModel(command);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            return await HandleCommandAsync(command);
        }
    }
}
