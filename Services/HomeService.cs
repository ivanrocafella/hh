using hh.Models;
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

        public async Task<List<Resume>> GetAll() => await _context.Resumes.Where(e => e.Set == true)
            .OrderByDescending(e => e.DateTimeUpdate).ToListAsync();
    }
}
