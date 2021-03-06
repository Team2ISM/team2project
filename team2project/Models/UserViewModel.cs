﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Events.Business.Models;
using System.Web.Mvc;

namespace team2project.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Введите ваше имя")]
        [StringLength(100, ErrorMessage = "Длина должна быть не больше 100 символов")]
        public string Name { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Введите вашу фамилию")]
        [StringLength(100, ErrorMessage = "Длина должна быть не больше 100 символов")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Введите e-mail")]
        [EmailAddress(ErrorMessage = "Введите e-mail")]
        [StringLength(150, ErrorMessage = "Длина должна быть не больше 150 символов")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Пароль должен быть больше 6 символов", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Пароль должен быть больше 6 символов", MinimumLength = 6)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string RepeatPassword { get; set; }

        [Required(ErrorMessage = "Введите город")]
        public int LocationId { get; set; }

        public string FullName
        {
            get
            {
                return Name + " " + Surname;
            }

            set { }
        }

        public IList<Role> Roles { get; set; }

        public UserViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}