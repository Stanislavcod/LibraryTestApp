using Library.BusinessLogic.Services.Contracts;
using Library.Common.ModelsDto;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace LibraryTestApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthController(IAuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("register")]
        public IActionResult Register([FromForm] UserDto request, [FromForm] string confirmPassword)
        {
            _authService.Register(request, confirmPassword);
            return RedirectToAction("login");
        }
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost("login")]
        public IActionResult Login([FromForm] UserDto request)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, request.Login) };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Book");
        }
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("login");
        }
    }
}
