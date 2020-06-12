using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using System.ComponentModel.DataAnnotations;

namespace WM.Northwind.Entities.Concrete.Authorization
{
    public class User : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Kulllanıcı Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz..!")]
        public string UserName { get; set; }

        [Display(Name = "Parolası")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz..!")]
        [DataType(DataType.Password)]
        [StringLength(64, ErrorMessage = "Şifre en az 6(altı) karakter olmalıdır..!", MinimumLength = 6)]
        public string Password { get; set; }

        //[Display(Name = "Parola Doğrulama")]
        //[Required(ErrorMessage = "{0} alanı boş bırakılamaz..!")]
        //[DataType(DataType.Password)]
        //[Compare("Password")]
        //[StringLength(64, ErrorMessage = "Şifre en az 6(altı) karakter olmalıdır..!", MinimumLength = 6)]
        //public string PasswordConfirm { get; set; }

        //      No numbers and the following characters are invalid:
        //      Should not have more than 3 words
        //      No preceding or trailing spaces
        //      Minimum of 2 characters
        [Display(Name = "Adı")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz..!")]
        [RegularExpression(@"^(?=(?:[^A-Za-z]*[A-Za-z]){2})(?![^\d~`?!^*¨ˆ;@=$%{}\[\]|\\\/<>#“.,]*[\d~`?!^*¨ˆ;@=$%{}\[\]|\\\/<>#“.,])\S+(?: \S+){0,2}$", ErrorMessage = "Lütfen 0 dan büyük bir sayı giriniz..!")]
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

        [Display(Name = "Telefon Nu.")]
        public string TelefonNo { get; set; }
        [Display(Name = "Telefon")]
        public string TelefonNumarasi => new Iletisim().GetTelefonNumarasi();

        public string CihazId { get; set; }

        public virtual List<UserRole> UserRoles { get; set; }
        public virtual List<UserEczaneOda> UserEczaneOdalar { get; set; }
        public virtual List<UserNobetUstGrup> UserNobetUstGruplar { get; set; }
        public virtual List<UserEczane> UserEczaneler { get; set; }
        public virtual List<EczaneNobetDegisim> EczaneNobetDegisimler { get; set; }
        public virtual List<EczaneNobetDegisimArz> EczaneNobetDegisimArzlar { get; set; }
        public virtual List<EczaneNobetDegisimTalep> EczaneNobetDegisimTalepler { get; set; }
        public virtual List<EczaneNobetSanalSonuc> EczaneNobetSanalSonuclar { get; set; }
    }
}
