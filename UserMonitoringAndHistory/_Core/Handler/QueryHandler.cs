using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Threading.Tasks;
using UserMonitoringAndHistory._Core.CallResults;
using UserMonitoringAndHistory._Core.Handler;
using UserMonitoringAndHistory.Data;

namespace UserMonitoringAndHistory
{
    public abstract class QueryHandler<TQuery, TResult> : CallResultShortcuts, IHandler 
        where TResult : CallResult, new() 
        where TQuery : Query
    {
        protected ApplicationDbContext DB { get; }

        protected QueryHandler(ApplicationDbContext db)
        {
            DB = db;
        }

        protected abstract Task<TResult> HandleCommandAsync(TQuery query);

        private TResult ValidateModel(TQuery query)
        {
            if (!query.IsValid())
            {
                var result = new TResult();
                var validationResult = Result.ValidationFail(query);
                result.SetResult(validationResult);
                return result;
            }

            return new TResult();
        }

        public async Task<TResult> HandleAsync(TQuery query)
        {
            var validationResult = ValidateModel(query);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            return await HandleCommandAsync(query);
        }
    }
}
