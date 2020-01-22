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
    public class EczaneGrupDetay : IComplexType
    {
        public int Id { get; set; }
        [Display(Name = "Eczane")]
        public string EczaneAdi { get; set; }
        [Display(Name = "Eczane Grubu")]
        public string EczaneGrupTanimAdi { get; set; }
        [Display(Name = "Nöbet Grubu")]
        public string NobetGrupAdi { get; set; }
        [Display(Name = "Eczane Grup Tanım Id")]
        public int EczaneGrupTanimId { get; set; }
        [Display(Name = "Bitiş Tarihi")]
        public DateTime? EczaneGrupTanimBitisTarihi { get; set; }
        //public DateTime? EczaneGrupBitisTarihi { get; set; }
        public int NobetGrupId { get; set; }
        public int EczaneGrupTanimTipId { get; set; }
        public string EczaneGrupTanimTipAdi { get; set; }
        public int NobetUstGrupId { get; set; }
        public int ArdisikNobetSayisi { get; set; }
        public int AyniGunNobetTutabilecekEczaneSayisi { get; set; }
        public int EczaneId { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public bool PasifMi { get; set; }
        public bool EczaneGrupTanimPasifMi { get; set; }
        public bool Checked { get; set; }

        public int NobetGorevTipId { get; set; }
        [Display(Name = "Görev Tipi")]
        public string NobetGorevTipAdi { get; set; }
        public List<EczaneNobetGrup> EczaneNobetGruplar { get; set; }
        public string EczaneAdiBakilan { get; set; }
        public int NobetGrupGorevTipIdFrom { get; set; }
        public int NobetGrupGorevTipIdTo { get; set; }

        public EczaneGrupDetay Clone()
        {
            return (EczaneGrupDetay)MemberwiseClone();
        }

        //public override OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik> Clone()
        //{
        //    return (OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>)MemberwiseClone();
        //}
    }
}
