using hh.Interfaces;
using hh.Models;
using hh.Services;
using hh.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hh.Controllers
{
    public class VacancyController : Controller
    {
        private readonly VacanciesService _Vservice;
        private readonly IAccountService<Account, IFormFile,
           LoginViewModel, Microsoft.AspNetCore.Identity.SignInResult,
           AccountViewModel, RegisterViewModel> _Aservice;
        private ApplicationContext _context;

        public VacancyController(VacanciesService Vservice, IAccountService<Account, IFormFile, LoginViewModel,
                Microsoft.AspNetCore.Identity.SignInResult, AccountViewModel, RegisterViewModel> Aservice, ApplicationContext context)
        {
            _Vservice = Vservice;
            _Aservice = Aservice;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Vacancy vacancy)
        {
            await _Vservice.MakeVacancy(vacancy);
            Account account = await _Aservice.GetAccountAsyncById(vacancy.AccountId);
            return RedirectToAction("PrivateCabinet", "Account", new { name = account.UserName });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categories = await _Vservice.GetCategories();
            return View(await _Vservice.VacancyForEdit(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Vacancy vacancy)
        {
            if (vacancy != null)
            {
                await _Vservice.EditVacancy(vacancy);
                Account account = await _Aservice.GetAccountAsyncById(vacancy.AccountId);
                return RedirectToAction("PrivateCabinet", "Account", new { name = account.UserName });
            }
            else
                return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Vacancy vacancy = await _Vservice.Update(id);
            Account account = await _Aservice.GetAccountAsyncById(vacancy.AccountId);
            return RedirectToAction("PrivateCabinet", "Account", new { name = account.UserName });
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id) => View(await _Vservice.GetResume(id));

        [HttpGet]
        public async Task<IActionResult> Set(int id)
        {
            await _Vservice.Set(id);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> SetOff(int id)
        {
            await _Vservice.SetOff(id);
            return RedirectToAction("Index", "Home");
        }

        public async Task<JsonResult> AllVacancies(int curPage, int itemsPerPage)
        {
            VacancyPageViewModel vacancyPage = await _Vservice.AllVacancies(curPage, itemsPerPage);
            return Json(new { vacancyPage });
        }
    }
}
