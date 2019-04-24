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
    public class AnahtarListe : IComplexType
    {
        public int Id { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public string EczaneAdi { get; set; }
        public int NobetGrupId { get; set; }
        public int NobetSayisi { get; set; } //nöbet turu. tür değil. (örnek: hafta içi nöbet turu)
        public string NobetGrupAdi { get; set; }
        public string GunGrup { get; set; }
        public DateTime Tarih { get; set; }
        public DateTime NobetUstGrupBaslamaTarihi { get; set; }
        public DateTime EczaneNobetGrupBaslamaTarihi { get; set; }
        public int GunGrupId { get; set; }
    }
}
