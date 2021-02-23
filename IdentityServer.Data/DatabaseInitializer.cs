using System;
using System.Threading.Tasks;
using System.Security.Claims;
using IdentityServer.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Data
{
    public class DatabaseInitializer
    {
        public static async Task Init(IServiceProvider serviceProvider)
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

            var result = await userManager.CreateAsync(user, "Vbrhjcthdbc98@");
            if (result.Succeeded)
            {
                await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator"));
            }

            var result2 = await userManager.CreateAsync(user2, "Vbrhjcthdbc98@");
            if (result2.Succeeded)
            {
                await userManager.AddClaimAsync(user2, new Claim(ClaimTypes.Role, "Administrator"));
            }

        }
    }
}
