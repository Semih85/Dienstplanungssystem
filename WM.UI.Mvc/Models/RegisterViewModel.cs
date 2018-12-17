using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WM.Northwind.Entities.Concrete.Authorization;

namespace WM.UI.Mvc.Models
{
    public class RegisterViewModel
    {
        public User User { get; set; }
        
        //[Display(Name = "Parolanız (Tekrar)")]
        //[Compare("Password", ErrorMessage = "{0} ve {1} alanı aynı olmalıdır")]
        //public string PasswordVreify { get; set; }

    }

}