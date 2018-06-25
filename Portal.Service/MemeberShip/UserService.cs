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
        private readonly UserManager<AppUser> _userManager;

        #region Ctor

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        #endregion


        public List<AppUser> GetUsers()
        {
            return _userManager.Users.ToList();
        }

        public void CreateUser(AppUser user)
        {
            var result=_userManager.CreateAsync(user);
        }
    }
}
