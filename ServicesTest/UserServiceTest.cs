using System;
using Microsoft.AspNetCore.Identity;
using Portal.core.Membership;
using Portal.Service.MemeberShip;
using Xunit;

namespace ServicesTest
{
    public class UserServiceTest
    {
        private UserManager<AppUser> _userManager;


        public UserServiceTest(UserManager<AppUser> userManager )
        {
            _userManager = userManager;
        }
        [Fact]
        public void CreateUserTest()
        {
            var userService = new UserService(_userManager);

            userService.CreateUser(new AppUser()
            {
                UserName = "vahid",
                PasswordHash = "123456"
            });
        }
    }
}
