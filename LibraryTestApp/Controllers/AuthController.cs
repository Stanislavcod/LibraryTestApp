using Library.BusinessLogic.Services.Contracts;
using Library.Common.ModelsDto;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Library.Model.DatabaseContext;

namespace LibraryTestApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDatabaseContext _context;
        public AuthController(IAuthService authService, IHttpContextAccessor httpContextAccessor, ApplicationDatabaseContext context)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
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
            try
            {
                var user = _context.Users.FirstOrDefault(u=> u.Login == request.Login);

                _authService.Login(request);

                var httpContext = _httpContextAccessor.HttpContext;

                var claims = new List<Claim> { new Claim(ClaimTypes.Name, request.Login), new Claim(ClaimTypes.Role, user.Role.Name) };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Book");
            }
            catch
            {
                return BadRequest();
            }
        }
        
        [HttpPost("logout"), Authorize]
        public IActionResult Logout()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("login");
        }
    }
}
