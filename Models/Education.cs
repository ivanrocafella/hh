using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hh.Models
{
    public class Education
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Название учебного заведения")]
        public string Name { get; set; }

        [Display(Name = "Дата начала")]
        [DataType(DataType.DateTime)]
        public DateTime DateStart { get; set; }

        [Display(Name = "Дата завершения")]
        [DataType(DataType.DateTime)]
        public DateTime DateEnd { get; set; }

        public Account Account { get; set; }
        public string AccountId { get; set; }
        public Resume Resume { get; set; }
        public int ResumeId { get; set; }
    }
}
