using hh.Models;
using hh.Services;
using hh.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace hh.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Account> _userManager;
        private ApplicationContext _context;

        public HomeController(ILogger<HomeController> logger,
            UserManager<Account> userManager, ApplicationContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            Account account = await _userManager.GetUserAsync(HttpContext.User);
            List<Resume> resumes = new List<Resume>();
            Account AccountFromContext = new Account();
            if (account != null)
            {
                resumes = await _context.Resumes.Where(e => e.Set == true && e.AccountId == account.Id)
                               .OrderByDescending(e => e.DateTimeUpdate).ToListAsync();               
                AccountFromContext = await _context.Accounts.Include(e => e.Resumes)
                              .FirstOrDefaultAsync(e => e.Id == account.Id);
            }
            List<Vacancy> vacancies = await _context.Vacancies.Where(e => e.Set == true)
                  .OrderByDescending(e => e.DateTimeUpdate).ToListAsync();
            
            ResumeVacancyViewModel resumeVacancy = new ResumeVacancyViewModel
            {
                Resumes = resumes,
                Vacancies = vacancies
            };          
            bool Bool;
            bool BoolSet = false;
            if (AccountFromContext.Resumes.Count > 0)
                Bool = true;
            else
                Bool = false;
            int i = 0;
            foreach (var item in resumes)
            {
                if (item.AccountId == account.Id)
                    i++;
                if (i > 0)
                    BoolSet = true;
            }
            ViewBag.Bool = Bool;
            ViewBag.BoolSet = BoolSet;
            return View(resumeVacancy);
        }
          


    }
}
