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
    public class NobetUstGrupGunGrupDetay : IComplexType
    {
        public int Id { get; set; }
        public int GunGrupId { get; set; }
        public int NobetUstGrupId { get; set; }
        public string Aciklama { get; set; }
        public string GunGrupAdi { get; set; }
        public string NobetUstGrupAdi { get; set; }
        public int AmacFonksiyonuKatsayisi { get; set; }
    }
}