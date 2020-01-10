using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.Northwind.Entities.Concrete
{
    public class Iletisim
    {
        public string Adres { get; set; }

        [Display(Name = "Telefon Nu.")]
        public string TelefonNo { get; set; }
        [Display(Name = "Telefon")]
        public string TelefonNumarasi => GetTelefonNumarasi();

        [Display(Name = "E-Posta")]
        public string MailAdresi { get; set; }
        [Display(Name = "Web Sitesi")]
        public string WebSitesi { get; set; }

        private string GetTelefonNumarasi(string tel)
        {
            if (tel != null && tel.Length == 10)
            {
                var ilkUc = tel.Substring(0, 3);
                var ikinciUc = tel.Substring(3, 3);
                var ilkIki = tel.Substring(6, 2);
                var ikinciIki = tel.Substring(8, 2);

                return $"0 {ilkUc} {ikinciUc} {ilkIki} {ikinciIki}";
            }
            else
            {
                return tel;
            }
        }

        public string GetTelefonNumarasi()
        {
            if (TelefonNo != null && TelefonNo.Length == 10)
            {
                var ilkUc = TelefonNo.Substring(0, 3);
                var ikinciUc = TelefonNo.Substring(3, 3);
                var ilkIki = TelefonNo.Substring(6, 2);
                var ikinciIki = TelefonNo.Substring(8, 2);

                return $"0 {ilkUc} {ikinciUc} {ilkIki} {ikinciIki}";
            }
            else
            {
                return TelefonNo;
            }
        }
    }
}
