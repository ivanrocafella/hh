using hh.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hh.Controllers
{
    public class ValidationController : Controller
    {
        private ApplicationContext _context;
        private readonly UserManager<Account> _userManager;

        public ValidationController(ApplicationContext context, UserManager<Account> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [AcceptVerbs("GET", "POST")]
        public bool CheckExistAccount(string UserName, string Email) => !_context.Accounts.Any(e => e.UserName == UserName || e.Email == Email);

        [AcceptVerbs("GET", "POST")]
        public async Task<bool> CheckExistAccountForEdit(string UserName, string Email)
        {
            Account curAccount = await _userManager.GetUserAsync(HttpContext.User);
            List<Account> accounts = _context.Accounts.ToList();
            accounts.Remove(curAccount);
            return !accounts.Any(e => e.UserName == UserName || e.Email == Email);
        }

        [AcceptVerbs("GET", "POST")]
        public bool CheckExistAccountForLogin(string EmailLogin) => _context.Accounts.Any(e => e.UserName == EmailLogin || e.Email == EmailLogin);
    }
}
