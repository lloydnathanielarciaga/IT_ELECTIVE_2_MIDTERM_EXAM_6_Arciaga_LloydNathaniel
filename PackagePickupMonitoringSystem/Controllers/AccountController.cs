using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PackagePickupMonitoringSystem.Models;
using PackagePickupMonitoringSystem.Repositories;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PackagePickupMonitoringSystem.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            User loggedInUser = UserRepository.GetUserByUsername(username);

            if (loggedInUser != null && loggedInUser.Password == password)
            {
                List<Claim> userClaims = new List<Claim>();
                userClaims.Add(new Claim(ClaimTypes.Name, loggedInUser.Username));
                userClaims.Add(new Claim("FullName", loggedInUser.FirstName + " " + loggedInUser.LastName));

                ClaimsIdentity identity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Package");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt. Try again.");
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                User existing = UserRepository.GetUserByUsername(newUser.Username);
                if (existing != null)
                {
                    ModelState.AddModelError("Username", "That username is already taken!");
                    return View(newUser);
                }

                UserRepository.AddUser(newUser);
                return RedirectToAction("Login");
            }

            return View(newUser);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}