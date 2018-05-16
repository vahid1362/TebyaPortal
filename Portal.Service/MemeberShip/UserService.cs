using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Portal.core.Membership;

namespace Portal.Service.MemeberShip
{
 public   class UserService
    {
        private readonly UserManager<User> _userManager;

        #region Ctor

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        #endregion


        public List<User> GetUsers()
        {
            return _userManager.Users.ToList();
        }

        public void CreateUser(User user)
        {
            var result=_userManager.CreateAsync(user);
        }
    }
}
