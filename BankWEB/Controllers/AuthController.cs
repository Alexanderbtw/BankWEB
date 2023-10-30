using BankWEB.DataContext;
using BankWEB.Interfaces;
using BankWEB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace BankWEB.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IBankData _bankData;
        private readonly IMemoryCache _cache;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IBankData bankData, IMemoryCache memoryCache)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _bankData = bankData;
            _cache = memoryCache;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserLogin());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(model.UsernameProp, model.Password, false, lockoutOnFailure: false);
                if (loginResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "This user is not found");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserReg());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserReg model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UsernameProp };
                var createResult = await _userManager.CreateAsync(user, model.Password);

                if (createResult.Succeeded)
                {
                    var res = _bankData.AddClientAsync(new Client()
                    {
                        Username = model.UsernameProp,
                        Status = model.Status
                    });
                    if (await res == HttpStatusCode.OK) 
                    {
                        return RedirectToAction("Login", "Auth");
                    }
                    return View(model);
                }
                else
                {
                    foreach (var error in createResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            _cache.Remove(User.Identity.Name);
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
