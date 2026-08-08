using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PackagePickupMonitoringSystem.Models;
using PackagePickupMonitoringSystem.Repositories;
using System.Security.Claims;

namespace PackagePickupMonitoringSystem.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = UserRepository.GetUserByUsername(username);

            if (user != null && user.Password == password)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("FullName", $"{user.FirstName} {user.LastName}")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Package");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }

        [HttpGet]
        public IActionResult Register() => View(); 

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid) 
            {
                if (UserRepository.GetUserByUsername(user.Username) != null)
                {
                    ModelState.AddModelError("Username", "Username already exists.");
                    return View(user);
                }

                UserRepository.AddUser(user);
                return RedirectToAction("Login");
            }
            return View(user);
        }

        public async Task<IActionResult> Logout() 
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}