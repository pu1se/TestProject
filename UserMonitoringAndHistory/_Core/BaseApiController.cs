using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserMonitoringAndHistory
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected string CurrentUserId
        {
            get
            {
                if (User?.Identity?.IsAuthenticated ?? false)
                {
                    return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                }

                return null;
            }
        }
    }
}
