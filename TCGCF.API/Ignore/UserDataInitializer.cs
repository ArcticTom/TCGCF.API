using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using TCGCF.API.Entities;

namespace TCGCF.API.Ignore
{
    public class UserDataInitializer
    {
        private RoleManager<IdentityRole> _roleMgr;
        private UserManager<User> _userMgr;

        public UserDataInitializer(UserManager<User> userMgr, RoleManager<IdentityRole> roleMgr)
        {
            _userMgr = userMgr;
            _roleMgr = roleMgr;
        }

        public async Task Seed()
        {
            //check if user exists
            var user = await _userMgr.FindByNameAsync("kujanto");

            if (user == null)
            {
                //add role
                if (!(await _roleMgr.RoleExistsAsync("Administrator")))
                {
                    var role = new IdentityRole("Administrator");
                    await _roleMgr.CreateAsync(role);
                    await _roleMgr.AddClaimAsync(role, new System.Security.Claims.Claim("IsAdmin", "True"));
                }

                // Add User
                user = new User()
                {
                    UserName = "kujanto",
                    FirstName = "Tommi",
                    LastName = "Kujanen",
                    Email = "tommi.kujanen@gmail.com"
                };

                var userResult = await _userMgr.CreateAsync(user, "P@ssw0rd!");
                var roleResult = await _userMgr.AddToRoleAsync(user, "Administrator");
                var claimResult = await _userMgr.AddClaimAsync(user, new System.Security.Claims.Claim("SuperUser", "True"));

                if (!userResult.Succeeded || !roleResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }

            }
        }
    }
}

