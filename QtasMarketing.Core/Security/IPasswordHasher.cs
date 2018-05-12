using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace QtasMarketing.Core.Security
{
    public interface IPasswordHasher<TUser> where TUser : class
    {
        string HashPassword(TUser user, string password);

        PasswordVerificationResult VerifyHashedPassword(
            TUser user, string hashedPassword, string providedPassword);
    }
}
