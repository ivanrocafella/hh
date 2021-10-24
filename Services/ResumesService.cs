using hh.Models;
using hh.ViewModels;
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

        public async Task<ResumeViewModel> GetResume(int id)
        {
            Resume resume = await _context.Resumes.Include(e => e.Educations)
                .Include(e => e.Jobs).Include(e => e.Account).FirstOrDefaultAsync(e => e.Id == id);
            List<Job> jobs = await _context.Jobs.Where(e => e.ResumeId == resume.Id).ToListAsync();
            List<Education> educations = await _context.Educations.Where(e => e.ResumeId == resume.Id).ToListAsync();
            ResumeViewModel resumeView = new ResumeViewModel
            {
                Resume = resume,
                Educations = educations,
                Jobs = jobs
            };
            return resumeView;  
        }

        public async Task<Resume> ResumeForEdit(int id) => await _context.Resumes.FirstOrDefaultAsync(e => e.Id == id);

        public async Task<List<Category>> GetCategories() => await _context.Categories.ToListAsync();

        public async Task EditResume(Resume resume)
        {
            _context.Resumes.Update(resume);
            await _context.SaveChangesAsync();
        }

        public async Task MakeJob(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
        }

        public async Task MakeEduc(Education education)
        {
            await _context.Educations.AddAsync(education);
            await _context.SaveChangesAsync();
        }

        public async Task<Resume> Update(int id)
        {
            Resume resume = await _context.Resumes.FirstOrDefaultAsync(e => e.Id == id);
            resume.DateTimeUpdate = DateTime.Now;
            _context.Resumes.Update(resume);
            await _context.SaveChangesAsync();
            return resume;
        }

        public async Task Set(int id)
        {
            Resume resume = await _context.Resumes.FirstOrDefaultAsync(e => e.Id == id);
            resume.Set = true;
            _context.Resumes.Update(resume);
            await _context.SaveChangesAsync();
        }

        public async Task SetOff(int id)
        {
            Resume resume = await _context.Resumes.FirstOrDefaultAsync(e => e.Id == id);
            resume.Set = false;
            _context.Resumes.Update(resume);
            await _context.SaveChangesAsync();
        }
    }
}
