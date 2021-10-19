using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace hh.ViewModels
{
    public class AccountViewModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Укажите логин")]
        [Display(Name = "Логин")]
        [Remote(action: "CheckExistAccountForEdit", controller: "Validation", ErrorMessage = "Такой пользователь уже есть")]
        [JsonPropertyName("user_name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Укажите email")]
        [EmailAddress(ErrorMessage = "Введите email корректно")]
        [Display(Name = "Email")]
        [Remote(action: "CheckExistAccountForEdit", controller: "Validation", ErrorMessage = "Такой пользователь уже есть")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите номер телефона")]
        [RegularExpression(@"\d{4}-\d{3}-\d{3}$", ErrorMessage = "Номер телефона должен иметь формат xxxx-xxx-xxx")]
        [Display(Name = "Номер телефона")]
        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        [JsonPropertyName("password_confirm")]
        public string PasswordConfirm { get; set; }

        [JsonPropertyName("avatar")]
        public string Avatar { get; set; }
    }
}
