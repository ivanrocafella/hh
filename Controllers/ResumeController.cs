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

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Resume resume = await _Rservice.Update(id);
            Account account = await _Aservice.GetAccountAsyncById(resume.AccountId);
            return RedirectToAction("PrivateCabinet", "Account", new { name = account.UserName });
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id) => View(await _Rservice.GetResume(id));

        [HttpPost]
        public async Task<IActionResult> AddJob(Job job)
        {
            await _Rservice.MakeJob(job);
            return RedirectToAction("Detail", new { id = job.ResumeId });
        }

        [HttpPost]
        public async Task<IActionResult> AddEduc(Education education)
        {
            await _Rservice.MakeEduc(education);
            return RedirectToAction("Detail", new { id = education.ResumeId });
        }

        
    }
}
