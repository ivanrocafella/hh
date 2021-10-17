using hh.Models;
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

        public ValidationController(ApplicationContext context)
        {
            _context = context;
        }

        [AcceptVerbs("GET", "POST")]
        public bool CheckExistAccount(string word) => !_context.Accounts.Any(e => e.UserName == word || e.Email == word);
    }
}
