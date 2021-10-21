using hh.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hh.Services
{
    public class ResumesService
    {
        private ApplicationContext _context;

        public ResumesService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task MakeResume(Resume resume)
        {
            Account account = await _context.Accounts.
                FirstOrDefaultAsync(e => e.Id == resume.AccountId);
            Category category = await _context.Categories.
                FirstOrDefaultAsync(e => e.Name == resume.CategoryName);
            resume.Email = account.Email;
            resume.Telegram = account.Telegram;
            resume.Phone = account.PhoneNumber;
            if (category != null)
                resume.CategoryId = category.Id;
            await _context.Resumes.AddAsync(resume);
            await _context.SaveChangesAsync();
        }
    }
}
