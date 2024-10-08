using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using UserMonitoringAndHistory.Models;
using UserMonitoringAndHistory.Services.User;
using System.Security.Claims;

namespace UserMonitoringAndHistory.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserService _userService;

        public HomeController(UserService userService)
        {
            _userService = userService;
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<IActionResult> Index()
        {
            var getUserListResult = await _userService.GetUserList();
            return View(getUserListResult);
        }

        [HttpPost]
        public async Task<IActionResult> SetAsAdmin(string userId)
        {
            var getUserListResult = await _userService.GetUserList();
            return View("Index", getUserListResult);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var getUserListResult = await _userService.GetUserList();
            return View("Index", getUserListResult);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
