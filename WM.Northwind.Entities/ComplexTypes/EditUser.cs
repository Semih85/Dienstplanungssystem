using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Authorization;

namespace WM.Northwind.Entities.ComplexTypes
{
    public class EditUser : IComplexType
    {
        public int Id { get; set; }

        [Display(Name = "Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz..!")]
        public string FirstName { get; set; }

        [Display(Name = "Soyadı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz..!")]
        public string LastName { get; set; }

        [Display(Name = "E-Posta")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz..!")]
        [RegularExpression(@"^[a-zA-Z0-9_&.-]+@[a-zA-Z0-9.-]+\.[a-zA-Z0-9]{2,5}$", ErrorMessage = "Lütfen geçerli bir e-posta adresi yazınız..!")]
        public string Email { get; set; }

        [Display(Name = "Başlama Tarihi")]
        public DateTime BaslamaTarihi { get; set; }
        [Display(Name = "Bitiş Tarihi")]
        public DateTime? BitisTarihi { get; set; }


        [Display(Name = "Son Parola")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz..!")]
        [DataType(DataType.Password)]
        [StringLength(64, ErrorMessage = "Son Parola en az 6(altı) karakter olmalıdır..!", MinimumLength = 6)]
        public string PasswordLast { get; set; }

        [Display(Name = "Parola")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz..!")]
        [DataType(DataType.Password)]
        [StringLength(64, ErrorMessage = "Parola en az 6(altı) karakter olmalıdır..!", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Parola (Tekrar)")]
        [Compare("Password", ErrorMessage = "{0} ve {1} alanı aynı olmalıdır")]
        public string PasswordVerify { get; set; }

        public bool ParolaDegistir { get; set; }
        public string UserName { get; set; }
    }
}
