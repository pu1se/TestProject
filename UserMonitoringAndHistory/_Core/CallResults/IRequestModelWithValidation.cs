using System.Collections.Generic;

namespace UserMonitoringAndHistory
{
    public interface IRequestModelWithValidation
    {
        Dictionary<string, List<string>> GetValidationErrors();
    }
}
