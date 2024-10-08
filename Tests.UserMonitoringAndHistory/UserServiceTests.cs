using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserMonitoringAndHistory.Data;
using UserMonitoringAndHistory.Services.User;

namespace Tests.UserMonitoringAndHistory
{
    [TestClass]
    public class UserServiceTests : BaseServiceTests<UserService>
    {
        [TestMethod]
        public async Task If_call_RefreshLoginInfoCommandHandler___Then_User_login_info_will_be_updated()
        {
            // init.
            var userId = TestData.AdminUserId;
            var user = await DB.Users.FirstAsync(el => el.Id == userId);
            var oldCountLoginNumber = user.CountLoginNumber;
            var oldLastLoginDate = user.LastLoginDateUtc;


            // act.
            var callResult = await Service.RefreshLoginInfo(userId);
            var userAfterLoginInfoRefresh = await DB.Users.FirstAsync(el => el.Id == userId);


            // check.
            Assert.IsTrue(callResult != null);
            Assert.IsTrue(callResult.IsSuccess);

            var newLastLoginDate = userAfterLoginInfoRefresh.LastLoginDateUtc;
            var newCountLoginNumber = userAfterLoginInfoRefresh.CountLoginNumber;
            Assert.IsTrue(oldCountLoginNumber + 1 == newCountLoginNumber);

            if (oldLastLoginDate.HasValue)
            {
                Assert.IsTrue(oldLastLoginDate.Value < newLastLoginDate!.Value);
                Assert.IsTrue(newLastLoginDate!.Value < DateTime.UtcNow);
            }
        }
    }
}
