using hh.Interfaces;
using hh.Models;
using hh.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private ApplicationContext _context;
        private readonly IAccountService<Account, IFormFile, 
            LoginViewModel, Microsoft.AspNetCore.Identity.SignInResult, AccountViewModel, RegisterViewModel> service;

        public AccountController(UserManager<Account> userManager,
            SignInManager<Account> signInManager,
            IAccountService<Account, IFormFile, LoginViewModel,
                Microsoft.AspNetCore.Identity.SignInResult, AccountViewModel, RegisterViewModel> service, ApplicationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.service = service;
            _context = context;
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
                Account account = await service.MakeUserForReg(registerView, uploadImage);
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

        [HttpGet]
        public async Task<IActionResult> PrivateCabinet(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                AccountViewModel accountView = await service.GetUserbyName(name);
                if (accountView != null)
                {
                    ViewBag.Categories = await _context.Categories.ToListAsync();
                    return View(accountView);
                }                   
                else
                    return RedirectToAction("Index", "Home");
            }
            else
                return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AccountViewModel accountView, IFormFile uploadImage)
        {
            Account account = await service.MakeUserForEdit(accountView, uploadImage);
            _context.Accounts.Update(account);
            _context.SaveChanges();
            return RedirectToAction("PrivateCabinet", new { name = account.UserName });
        }

        [HttpGet]
        public async Task<IActionResult> PageCompany(string id) =>
            View(await service.GetAccountAsyncById(id)); 
    }
}
