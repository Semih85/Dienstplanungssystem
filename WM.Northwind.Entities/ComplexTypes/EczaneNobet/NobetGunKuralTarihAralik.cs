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
    public class NobetGunKuralTarihAralik : IComplexType
    {   
        public int NobetGunKuralId { get; set; }
        public string NobetGunKuralAdi { get; set; }
        public int GunSayisi { get; set; }
        public int GunGrupId { get; set; }
        public string GunGrupAdi { get; set; }
        public int KumulatifGunSayisi { get; set; }
        public double OrtalamaNobetSayisi { get; set; }
        public double KumulatifOrtalamaNobetSayisi { get; set; }
        public DateTime? KapanmaTarihi { get; set; }
        public List<TakvimNobetGrup> TakvimNobetGruplar { get; set; }
    }
}
