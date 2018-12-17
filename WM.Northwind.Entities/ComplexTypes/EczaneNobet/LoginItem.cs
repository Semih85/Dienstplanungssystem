using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class LoginItem
    {
        [Display(Name = "Eposta")]
        public string Email { get; set; }

        [Display(Name = "Parola")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int UserId { get; set; }

        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
