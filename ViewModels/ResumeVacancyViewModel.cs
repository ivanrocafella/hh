using hh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hh.ViewModels
{
    public class ResumeVacancyViewModel
    {
       public List<Resume> Resumes { get; set; }
       public List<Vacancy> Vacancies { get; set; }
    }
}
