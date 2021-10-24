using hh.Models;
using hh.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hh.Services
{
    public class HomeService
    {
        private ApplicationContext _context;

        public HomeService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ResumeVacancyViewModel> GetAll()
        {
            List<Resume> resumes = await _context.Resumes.Where(e => e.Set == true)
                 .OrderByDescending(e => e.DateTimeUpdate).ToListAsync();
            List<Vacancy> vacancies = await _context.Vacancies.Where(e => e.Set == true)
                  .OrderByDescending(e => e.DateTimeUpdate).ToListAsync();
            ResumeVacancyViewModel resumeVacancy = new ResumeVacancyViewModel
            {
                Resumes = resumes,
                Vacancies = vacancies
            };
            return resumeVacancy;
        }
            
            

    }
}
