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
    public class ResumeController : Controller
    {
        private readonly ResumesService _Rservice;
        private readonly IAccountService<Account, IFormFile,
           LoginViewModel, Microsoft.AspNetCore.Identity.SignInResult,
           AccountViewModel, RegisterViewModel> _Aservice;

        public ResumeController(ResumesService Rservice, IAccountService<Account, IFormFile, LoginViewModel,
                Microsoft.AspNetCore.Identity.SignInResult, AccountViewModel, RegisterViewModel> Aservice)
        {
            _Rservice = Rservice;
            _Aservice = Aservice;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Resume resume)
        {
            await _Rservice.MakeResume(resume);
            Account account = await _Aservice.GetAccountAsyncById(resume.AccountId);
            return RedirectToAction("PrivateCabinet", "Account", new { name = account.UserName });
        }
    }
}
