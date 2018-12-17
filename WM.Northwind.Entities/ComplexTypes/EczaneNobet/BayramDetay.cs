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
    public class BayramDetay : IComplexType
    {
        public int Id { get; set; }
        public int TakvimId { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public int NobetGorevTipId { get; set; }
        public int NobetUstGrupId { get; set; }
        public int NobetGrupId { get; set; }
        public int BayramTurId { get; set; }
        public int NobetGunKuralId { get; set; }
        public DateTime Tarih { get; set; }
        public string NobetGrupGorevTipAdi { get; set; }
        public string NobetGunKuralAdi { get; set; }
        public string NobetGrupAdi { get; set; }
        public string BayramTurAdi { get; set; }
    }
}