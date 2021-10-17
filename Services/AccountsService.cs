using hh.Interfaces;
using hh.Models;
using hh.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace hh.Services
{
    public class AccountsService : IAccountService<Account, RegisterViewModel, IFormFile, LoginViewModel, SignInResult>
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private IWebHostEnvironment _appEnvironment;

        public AccountsService(UserManager<Account> userManager, SignInManager<Account> signInManager,
          IWebHostEnvironment appEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appEnvironment = appEnvironment;
        }

        public async Task<SignInResult> TakeLoginEmail(LoginViewModel loginView)
        {
            Account accountByName = await _userManager.FindByNameAsync(loginView.EmailLogin);
            Account accountByEmail = await _userManager.FindByEmailAsync(loginView.EmailLogin);
            Account ByEmailOrName;

            if (accountByName != null)
                ByEmailOrName = accountByName;
            else
                ByEmailOrName = accountByEmail;
           
            SignInResult result = await _signInManager.PasswordSignInAsync(
            ByEmailOrName,
            loginView.Password,
            loginView.RememberMe,
            false);           
            return result;
        }


        public async Task<Account> MakeUser(RegisterViewModel registerView, IFormFile uploadImage)
        {
            string pathImage;
            if (uploadImage != null)
            {
                pathImage = "/files/" + uploadImage.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathImage, FileMode.Create))
                {
                    await uploadImage.CopyToAsync(fileStream);
                }
            }
            else
                pathImage = "/files/default.png";
            Account account = new Account
            {
                Email = registerView.Email,
                UserName = registerView.UserName,
                Avatar = pathImage,
            };
            return account;
        }

    }
}
