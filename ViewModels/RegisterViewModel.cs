using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hh.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Укажите логин")]
        [Display(Name = "Логин")]
        [Remote(action: "CheckExistAccount", controller: "Validation", ErrorMessage = "Такой пользователь уже есть")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Укажите email")]
        [EmailAddress(ErrorMessage = "Введите email корректно")]
        [Display(Name = "Email")]
        [Remote(action: "CheckExistAccount", controller: "Validation", ErrorMessage = "Такой пользователь уже есть")]
        public string Email { get; set; }

        public string Role { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите номер телефона")]
        [RegularExpression(@"\d{4}-\d{3}-\d{3}$", ErrorMessage = "Номер телефона должен иметь формат xxxx-xxx-xxx")]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
