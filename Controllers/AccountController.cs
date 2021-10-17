using hh.Interfaces;
using hh.Models;
using hh.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hh.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly IAccountService<Account, RegisterViewModel, IFormFile, 
            LoginViewModel, Microsoft.AspNetCore.Identity.SignInResult> service;

        public AccountController(UserManager<Account> userManager,
            SignInManager<Account> signInManager,
            IAccountService<Account, RegisterViewModel, IFormFile, LoginViewModel,
                Microsoft.AspNetCore.Identity.SignInResult> service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.service = service;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerView, IFormFile uploadImage)
        {
            if (ModelState.IsValid)
            {
                Account account = await service.MakeUser(registerView, uploadImage);
                var result = await _userManager.CreateAsync(account, registerView.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(account, registerView.Role);
                    await _signInManager.SignInAsync(account, false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(registerView);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null) =>
                     View(new LoginViewModel { ReturnUrl = returnUrl });


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginView)
        {
            if (ModelState.IsValid)
            {
               var result = await service.TakeLoginEmail(loginView);
               if (result.Succeeded)
               {
                   if (!string.IsNullOrEmpty(loginView.ReturnUrl) && Url.IsLocalUrl(loginView.ReturnUrl))
                       return Redirect(loginView.ReturnUrl);
                    return RedirectToAction("Index", "Home");
               }
               ModelState.AddModelError("", "Неправильный логин, электронная почта и (или) пароль");
            }
            return View(loginView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
