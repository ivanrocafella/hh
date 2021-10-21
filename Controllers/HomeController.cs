using hh.Models;
using hh.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly HomeService _service;

        public HomeController(ILogger<HomeController> logger, HomeService service)
        {
            _logger = logger;
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index() => View(await _service.GetAll());


    }
}
