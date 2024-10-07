using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserMonitoringAndHistory.Data;

namespace UserMonitoringAndHistory.Controllers
{
    public class CurrentUserController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("login-callback")]
        public IActionResult LoginCallback()
        {

            return Ok();
        }
    }
}
