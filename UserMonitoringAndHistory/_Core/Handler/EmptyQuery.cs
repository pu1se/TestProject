using System.Collections.Generic;
using UserMonitoringAndHistory._Core.CallResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UserMonitoringAndHistory
{
    public class EmptyQuery : BaseRequestModelWithValidation
    {
        public static EmptyQuery Value => new EmptyQuery();
        public override Dictionary<string, List<string>> GetValidationErrors()
        {
            return new Dictionary<string, List<string>>();
        }
    }
}
