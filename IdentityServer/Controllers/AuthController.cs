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
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(IIdentityServerInteractionService interaction, 
            IWebHostEnvironment environment, 
            UserManager<User> userManager, 
            SignInManager<User> signInManager)
        {
            _interaction = interaction;
            _environment = environment;
            _userManager = userManager;
            _signInManager = signInManager;
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

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (signInResult.Succeeded)
                {
                    return new JsonResult(new { IsOk = true, RedirectUrl = model.ReturnUrl });
                }
            }
            return Unauthorized();
        }

        [HttpPost]
        [EnableCors("user")]
        [Route("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User user = new User { 
                UserName = model.UserName, 
                Email = model.Email 
            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded) {
                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (signInResult.Succeeded)
                {
                    return Ok();
                }
            }
            return Unauthorized();
        }

        [EnableCors("user")]
        [Route("[action]")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutResult = await _interaction.GetLogoutContextAsync(logoutId);
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
            var message = await _interaction.GetErrorContextAsync(errorId);

            if (message != null)
            {
                if (!_environment.IsDevelopment())
                {
                    // only show in development
                    message.ErrorDescription = null;
                }
            }

            return Ok(message);
        }
    }
}