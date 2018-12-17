using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetIstatistikGunFarkiFrekans : IComplexType
    {
        public int EczaneNobetGrupId { get; set; }
        public int NobetGorevTipId { get; set; }
        public string NobetGorevTipAdi { get; set; }
        public int NobetSonucDemoTipId { get; set; }
        public int NobetGrupId { get; set; }
        public string NobetGrupAdi { get; set; }
        public int EczaneId { get; set; }
        public string EczaneAdi { get; set; }
        public string GunGrup { get; set; }
        public int GunFarki { get; set; }
        public int FrekanstakiEczaneSayisi { get; set; }
    }
}
