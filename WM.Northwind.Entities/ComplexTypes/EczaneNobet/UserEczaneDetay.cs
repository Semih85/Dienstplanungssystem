using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class UserEczaneDetay : IComplexType
    {
        public int Id { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }
        [Display(Name = "Eczane")]
        public string EczaneAdi { get; set; }
        [Display(Name = "Açıklama")]
        public int EczaneId { get; set; }
        public int UserId { get; set; }
        public int NobetUstGrupId { get; set; }
        public DateTime BaslamaTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
    }
}
