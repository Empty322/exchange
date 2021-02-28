using System.Threading.Tasks;
using IdentityServer.Data.Models;
using IdentityServer.ViewModels;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityServerInteractionService interaction;
        private readonly IWebHostEnvironment environment;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AuthController(IIdentityServerInteractionService interaction, 
            IWebHostEnvironment environment, 
            UserManager<User> userManager, 
            SignInManager<User> signInManager)
        {
            this.interaction = interaction;
            this.environment = environment;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpPost]
        [EnableCors("user")]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (signInResult.Succeeded)
                {
                    return new JsonResult(new { IsOk = true, RedirectUrl = model.ReturnUrl });
                }
            }
            return Unauthorized();
        }

        [EnableCors("user")]
        [Route("[action]")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await signInManager.SignOutAsync();
            var logoutResult = await interaction.GetLogoutContextAsync(logoutId);
            if (!string.IsNullOrEmpty(logoutResult.PostLogoutRedirectUri)) {
                return Redirect(logoutResult.PostLogoutRedirectUri);
            }
            return Ok();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Error(string errorId)
        {
            // retrieve error details from identityserver
            var message = await interaction.GetErrorContextAsync(errorId);

            if (message != null)
            {
                if (!environment.IsDevelopment())
                {
                    // only show in development
                    message.ErrorDescription = null;
                }
            }

            return Ok(message);
        }
    }
}