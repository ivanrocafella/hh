using hh.Models;
using hh.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hh.Services
{
    public class VacanciesService
    {
        private readonly ApplicationContext _context;

        public VacanciesService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task MakeVacancy(Vacancy vacancy)
        {
            Account account = await _context.Accounts.
                FirstOrDefaultAsync(e => e.Id == vacancy.AccountId);
            Category category = await _context.Categories.
                FirstOrDefaultAsync(e => e.Name == vacancy.CategoryName);
            vacancy.Email = account.Email;
            vacancy.Telegram = account.Telegram;
            vacancy.Phone = account.PhoneNumber;
            if (category != null)
                vacancy.CategoryId = category.Id;
            await _context.Vacancies.AddAsync(vacancy);
            await _context.SaveChangesAsync();
        }

        public async Task<Vacancy> GetResume(int id) => 
            await _context.Vacancies.Include(e => e.Account).FirstOrDefaultAsync(e => e.Id == id);

        public async Task<Vacancy> VacancyForEdit(int id) =>
            await _context.Vacancies.FirstOrDefaultAsync(e => e.Id == id);

        public async Task<List<Category>> GetCategories() => await _context.Categories.ToListAsync();

        public async Task EditVacancy(Vacancy vacancy)
        {
            _context.Vacancies.Update(vacancy);
            await _context.SaveChangesAsync();
        }


        public async Task<Vacancy> Update(int id)
        {
            Vacancy vacancy = await _context.Vacancies.FirstOrDefaultAsync(e => e.Id == id);
            vacancy.DateTimeUpdate = DateTime.Now;
            _context.Vacancies.Update(vacancy);
            await _context.SaveChangesAsync();
            return vacancy;
        }

        public async Task Set(int id)
        {
            Vacancy vacancy = await _context.Vacancies.FirstOrDefaultAsync(e => e.Id == id);
            vacancy.Set = true;
            _context.Vacancies.Update(vacancy);
            await _context.SaveChangesAsync();
        }

        public async Task SetOff(int id)
        {
            Vacancy vacancy = await _context.Vacancies.FirstOrDefaultAsync(e => e.Id == id);
            vacancy.Set = false;
            _context.Vacancies.Update(vacancy);
            await _context.SaveChangesAsync();
        }

        public async Task<VacancyPageViewModel> AllVacancies(int curPage, int itemsPerPage)
        {
            List<Vacancy> vacancies = await _context.Vacancies.OrderByDescending(e => e.DateTimeUpdate)
                  .Where(e => e.Set == true).ToListAsync();
            var maxPage = (int)Math.Ceiling((double)vacancies.Count / itemsPerPage);
            List<Vacancy> page = new List<Vacancy>();
            if (curPage == 1)
                page = vacancies.Take(itemsPerPage).ToList();
            else if (curPage > 1 && curPage < maxPage)
                page = vacancies.Skip((curPage - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            else
                page = vacancies.Skip((curPage - 1) * itemsPerPage).Take(vacancies.Count - ((curPage - 1) * itemsPerPage)).ToList();
            VacancyPageViewModel vacancyPage = new VacancyPageViewModel
            {
                Vacancies = page,
                CurPage = curPage,
                MaxPage = maxPage
            };
            return vacancyPage;
        }
    }
}
