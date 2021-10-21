using hh.Models;
using hh.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace hh.Services
{
    public class CategoriesService
    {
        private ApplicationContext _context;

        public CategoriesService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<CategoryAndCategories> GetCategories()
        {
            List<Category> categories = await _context.Categories.ToListAsync();
            CategoryAndCategories categoryAndCategories = new CategoryAndCategories { Categories = categories };
            return categoryAndCategories;
        }

        public async Task<Category> AddCateg(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            Category categoryDB = await _context.Categories.FirstOrDefaultAsync(e => e.Name == category.Name);
            return categoryDB;
        }
    }
}
