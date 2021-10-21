using hh.Models;
using hh.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hh.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoriesService service;

        public CategoryController(CategoriesService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(await service.GetCategories());
        [HttpPost]
        public async Task<JsonResult> AddCategory(Category category)
        { 
            Category categoryToJs =  await service.AddCateg(category);
            return Json(new { categoryToJs });
        }
        
    }
}
