using hh.Interfaces;
using hh.Models;
using hh.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace hh.Services
{
    public class AccountsService : IAccountService<Account, IFormFile,
        LoginViewModel, SignInResult, AccountViewModel, RegisterViewModel>
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private IWebHostEnvironment _appEnvironment;
        private ApplicationContext _context;

        public AccountsService(UserManager<Account> userManager, SignInManager<Account> signInManager,
          IWebHostEnvironment appEnvironment, ApplicationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appEnvironment = appEnvironment;
            _context = context;
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


        public async Task<Account> MakeUserForEdit(AccountViewModel accountView, IFormFile uploadImage)
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
                pathImage = accountView.Avatar;
           Account account = await _context.Accounts.FirstOrDefaultAsync(e => e.Id == accountView.Id);
           account.Email = accountView.Email;
           account.UserName = accountView.UserName;
           account.Avatar = pathImage;
           account.Role = accountView.Role;
           account.Name = accountView.Name;
           account.PhoneNumber = accountView.Phone;
            return account;
        }

        public async Task<Account> MakeUserForReg(RegisterViewModel registerView, IFormFile uploadImage)
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
                Role = registerView.Role,
                Name = registerView.Name,
                PhoneNumber = registerView.Phone
            };
            return account;
        }

        public async Task<AccountViewModel> GetUserbyName(string name)
        {
            Account account = await _context.Accounts.FirstOrDefaultAsync(e => e.UserName == name);
            AccountViewModel accountViewModel = new AccountViewModel
            {
                Id = account.Id,
                UserName = account.UserName,
                Email = account.Email,
                Role = account.Role,
                Name = account.Name,
                Phone = account.PhoneNumber,
                Avatar = account.Avatar,
                Password = account.PasswordHash,
                PasswordConfirm = account.PasswordHash
            };
            return accountViewModel;
        }
    }
}
