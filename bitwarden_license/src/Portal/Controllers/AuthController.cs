using System.Threading.Tasks;
using Bit.Portal.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bit.Portal.Controllers
{
    public class AuthController : Controller
    {
        private readonly EnterprisePortalTokenSignInManager _signInManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            EnterprisePortalTokenSignInManager signInManager,
            ILogger<AuthController> logger
            )
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet("~/login")]
        public async Task<IActionResult> Index(string userId, string token, string organizationId, string returnUrl)
        {
            var result = await _signInManager.TokenSignInAsync(userId, token, false);
            _logger.LogInformation("AuthController - result:\n{result}", result);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", "Home", new
                {
                    error = 2
                });
            }

            _logger.LogInformation("AuthController - Setting the Selected Organization Cookie");
            if (!string.IsNullOrWhiteSpace(organizationId))
            {
                Response.Cookies.Append("SelectedOrganization", organizationId, new CookieOptions { HttpOnly = true });
            }

            _logger.LogInformation("AuthController - Setting the returnUrl");
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            _logger.LogInformation("AuthController - Redirecting to Home because something unexpected happend");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("~/logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("LoggedOut");
        }

        [HttpGet("~/logged-out")]
        public IActionResult LoggedOut()
        {
            return View();
        }

        [HttpGet("~/access-denied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
