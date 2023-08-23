using ContactManager.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactManager.Controllers
{

    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(IsValidUser(model.Email, model.Password))
            {
                var claims = new[] { new Claim(ClaimTypes.Name, model.Email) };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Неверное имя пользователя или пароль.");
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private bool IsValidUser(string login, string password)
        {
            return login == "admin@gmail.com" && password == "admin";
        }
    }
}
