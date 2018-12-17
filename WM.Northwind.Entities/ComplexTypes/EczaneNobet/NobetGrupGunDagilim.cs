using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class NobetGrupGunDagilim
    {
        public string GunGrup { get; set; }
        public int NobetGrupId { get; set; }
        public string NobetGrupAdi { get; set; }
        public string NobetGrubu => $"{NobetGrupId} {NobetGrupAdi}";
        public DateTime SonNobetTarihi { get; set; }
        public string SonNobetTarihiAciklama { get; set; }
        public int NobetSayisiMax { get; set; }
        public int NobetSayisiMin { get; set; }
        public int NobetSayisiFarki => NobetSayisiMax - NobetSayisiMin;
        public int BorcluGunSayisiMax { get; set; }
        public int BorcluGunSayisiMin { get; set; }
        public int BorcluGunSayisiFarki => BorcluGunSayisiMax - BorcluGunSayisiMin;
    }
}
