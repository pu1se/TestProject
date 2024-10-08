
using System.Collections.Generic;

namespace UserMonitoringAndHistory
{
    public class EmptyQuery : Query
    {
        public static EmptyQuery Value => new EmptyQuery();
        public override Dictionary<string, List<string>> GetValidationErrors()
        {
            return new Dictionary<string, List<string>>();
        }
    }
}
