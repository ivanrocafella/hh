using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hh.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Позиция")]
        public string Position { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Зарплата")]
        public int Salary { get; set; }
        public string Email { get; set; }
        public string Telegram { get; set; }
        public string Phone { get; set; }
        public DateTime DateTimeCreate { get; set; }
        public DateTime DateTimeUpdate { get; set; }

        public Account Account { get; set; }
        public string AccountId { get; set; }
        public Category Category { get; set; }
        public int? CategoryId { get; set; }
        public List<Job> Jobs { get; set; }
        public List<Education> Educations { get; set; }

        public Resume()
        {
            DateTimeCreate = DateTime.Now;
            Jobs = new List<Job>();
            Educations = new List<Education>();
        }
    }
}
