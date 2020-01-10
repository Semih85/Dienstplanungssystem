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
    public class NobetUstGrupKisitDetayDegisimTakip : IComplexType, ICloneable
    {
        public NobetUstGrupKisitDetayDegisimTakip()
        {
            //NobetUstGrupKisitDetayOnce = new NobetUstGrupKisitDetay();
            //NobetUstGrupKisitDetaySonra = new NobetUstGrupKisitDetay();
            //SiraNumarasi++;
        }

        public NobetUstGrupKisitDetayDegisimTakip(int siraNumarasi)
        {
            SiraNumarasi = siraNumarasi + 1;
        }

        public NobetUstGrupKisitDetay NobetUstGrupKisitDetayOnce { get; set; }
        public NobetUstGrupKisitDetay NobetUstGrupKisitDetaySonra { get; set; }
        public int NobetUstGrupId { get; set; }
        public int SiraNumarasi { get; set; }
        public DateTime DegisimTarihi { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}