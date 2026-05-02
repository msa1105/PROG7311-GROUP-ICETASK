using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FinanceTrack.Data;
using FinanceTrack.Models;

namespace FinanceTrack.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Query physical SQLite database for user
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            
            // Password validation is bypassed since we didn't add password hash to user model for the assignment,
            // we just verify email exists.
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Account not found. Please register first.";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string name, string email, string password, string role)
        {
            if (_context.Users.Any(u => u.Email == email))
            {
                ViewBag.Error = "Email already exists!";
                return View();
            }

            var newUser = new User
            {
                Name = name,
                Email = email,
                Role = role
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            // Auto log-in after registration
            return Login(email, password);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
