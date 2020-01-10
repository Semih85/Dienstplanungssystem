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
    public class UserNobetUstGrupDetay : IComplexType
    {
        public int Id { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }
        [Display(Name = "Nöbet Üst Grup")]
        public string NobetUstGrupAdi { get; set; }
        [Display(Name = "Açıklama")]
        public int NobetUstGrupId { get; set; }
        public int UserId { get; set; }
        public DateTime BaslamaTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
    }
}
