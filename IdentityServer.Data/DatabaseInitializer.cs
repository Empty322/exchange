using System;
using System.Security.Claims;
using IdentityServer.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Data
{
    public class DatabaseInitializer
    {
        public static void Init(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var user = new User
            {
                UserName = "SedativeEffect"
            };
            var user2 = new User
            {
                UserName = "Empty"
            };

            var result = userManager.CreateAsync(user, "Vbrhjcthdbc98@").GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator")).GetAwaiter().GetResult();
            }

            var result2 = userManager.CreateAsync(user2, "Vbrhjcthdbc98@").GetAwaiter().GetResult();
            if (result2.Succeeded)
            {
                userManager.AddClaimAsync(user2, new Claim(ClaimTypes.Role, "Administrator")).GetAwaiter().GetResult();
            }

        }
    }
}
