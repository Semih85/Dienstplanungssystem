using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetSonucDetay2 : IComplexType
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }
        public bool YayimlandiMi { get; set; }
        public int TakvimId { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int NobetGorevTipId { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public int EczaneId { get; set; }
        public int NobetGrupId { get; set; }
        public int NobetUstGrupId { get; set; }
        public int NobetAltGrupId { get; set; }
        public DateTime EczaneNobetGrupBaslamaTarihi { get; set; }
        public DateTime? EczaneNobetGrupBitisTarihi { get; set; }
        public DateTime NobetUstGrupBaslamaTarihi { get; set; }
        public DateTime NobetGrupGorevTipBaslamaTarihi { get; set; }

        public string NobetGorevTipAdi { get; set; }
        public string NobetGrupAdi { get; set; }        
        public string EczaneAdi { get; set; }
        public string NobetAltGrupAdi { get; set; }
        public string EczaneninAcikOlduguSaatAraligi { get; set; }
        public DateTime? NobetAltGrupKapanmaTarihi { get; set; }
        public bool SanalNobetMi { get; set; }
        public string SanalNobetAciklama { get; set; }
    }
}

