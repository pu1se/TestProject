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
using UserMonitoringAndHistory.Services.User.Handlers.DeleteUser;
using UserMonitoringAndHistory.Services.User.Handlers.GetUserList;
using UserMonitoringAndHistory.Services.User.Handlers.SetToUserAdminRole;

namespace UserMonitoringAndHistory.Controllers
{
    public class HomeController : BaseController
    {
        private readonly UserService _userService;

        public HomeController(UserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var getUserListResult = await _userService.GetUserList(new GetUserListQuery
            {
                OnBehalfOfUserId = GetCurrentUserId()
            });
            return View(getUserListResult);
        }

        [HttpPost]
        public async Task<IActionResult> SetAsAdmin(string userId)
        {
            var setAdminResult = await _userService.SetToUserAdminRole(new SetToUserAdminRoleCommand
            {
                UserId = userId,
            });

            if (setAdminResult.IsFail)
            {
                return View("Index", new CallListResult<GetUserListItemResult>(setAdminResult.ErrorMessage, setAdminResult.ErrorType));
            }

            var getUserListResult = await _userService.GetUserList(new GetUserListQuery
            {
                OnBehalfOfUserId = GetCurrentUserId()
            });
            return View("Index", getUserListResult);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var deleteResult = await _userService.DeleteUser(new DeleteUserCommand
            {
                UserId = userId,
                OnBehalfOfUserId = GetCurrentUserId()
            });

            if (deleteResult.IsFail)
            {
                return View("Index", new CallListResult<GetUserListItemResult>(deleteResult.ErrorMessage, deleteResult.ErrorType));
            }

            var getUserListResult = await _userService.GetUserList(new GetUserListQuery
            {
                OnBehalfOfUserId = GetCurrentUserId()
            });
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
