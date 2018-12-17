using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetSonucDagilimlar
    {
        public string Yıl_Ay { get; set; }
        public int Yil { get; set; }
        public int Ay { get; set; }
        public string Gun { get; set; }
        public string GunTanim { get; set; }
        public string GunGrup { get; set; }
        public string NobetGrubu { get; set; }
        public string NobetAltGrubu { get; set; }
        public string GorevTipi { get; set; }
        public string Eczane { get; set; }
        public string Tarih { get; set; }
        public string Tarih2 { get; set; }
        public string MazereteNobet { get; set; }
        public int NobetTipId { get; set; }
        public string NobetTipi { get; set; }
        public string EczaneSonucAdi { get; set; }
        public string AyIkili { get; set; }
        public string Mevsim { get; set; }        
    }
}
