using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using hringr.Models;
using Microsoft.AspNet.Identity;

namespace hringr.Services
{
    public static class IdentityService
    {
        public static async Task<ApplicationUser> FindUserNameByEmailOrUserNameAsync
            (this UserManager<ApplicationUser> userManager, string userNameOrEmail, string password)
        {
            var userName = userNameOrEmail;
            if (userNameOrEmail.Contains("@"))
            {
                var userForEmail = await userManager.FindByEmailAsync(userNameOrEmail);

                if (userForEmail != null)
                {
                    userName = userForEmail.UserName;
                }
            }
            return await userManager.FindAsync(userName, password);
        }
    }
}