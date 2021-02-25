using System;
using System.Threading.Tasks;
using System.Security.Claims;
using IdentityServer.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace IdentityServer.Data
{
    public class DatabaseInitializer
    {
        public static async Task Init(IServiceProvider serviceProvider)
        {
            using (UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>())
            {
                if (!userManager.Users.Any(x => x.UserName == "SedativeEffect"))
                {
                    User user = new User { UserName = "SedativeEffect" };
                    IdentityResult result = await userManager.CreateAsync(user, "Vbrhjcthdbc98@");
                    if (result.Succeeded)
                        await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator"));
                }

                if (!userManager.Users.Any(x => x.UserName == "Empty"))
                {
                    User user = new User { UserName = "Empty" };
                    IdentityResult result = await userManager.CreateAsync(user, "test");
                    if (result.Succeeded)
                        await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator"));
                }
            }
        }
    }
}
