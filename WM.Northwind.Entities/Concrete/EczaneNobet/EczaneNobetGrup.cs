using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class EczaneNobetGrup : IEntity
    {
        public int Id { get; set; }
        public int EczaneId { get; set; }
        //public int NobetGrupId { get; set; }
        public int NobetGrupGorevTipId { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Başlangıç Tarihi")]
        public DateTime BaslangicTarihi { get; set; }
        //Bitiş tarihi null ise, bu eczane o grupta aktiftir.
        [Display(Name = "Bitiş Tarihi")]
        public DateTime? BitisTarihi { get; set; }
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
        public bool EnErkenTarihteNobetYazilsinMi { get; set; }

        public virtual Eczane Eczane { get; set; }
        public virtual NobetGrupGorevTip NobetGrupGorevTip { get; set; }
        public virtual List<EczaneNobetGrupAltGrup> EczaneNobetGrupAltGruplar { get; set; }
        public virtual List<EczaneNobetSonuc> EczaneNobetSonuclar { get; set; }
        public virtual List<EczaneNobetSonucAktif> EczaneNobetSonucAktifler { get; set; }
        public virtual List<EczaneNobetSonucDemo> EczaneNobetSonucDemolar { get; set; }
        public virtual List<EczaneNobetSonucPlanlanan> EczaneNobetSonucPlanlananlar { get; set; }
        public virtual List<AyniGunTutulanNobet> AyniGunTutulanNobetler1 { get; set; }
        public virtual List<AyniGunTutulanNobet> AyniGunTutulanNobetler2 { get; set; }
        public virtual List<EczaneNobetMazeret> EczaneNobetMazeretler { get; set; }
        public virtual List<EczaneNobetIstek> EczaneNobetIstekler { get; set; }
        public virtual List<EczaneNobetDegisim> EczaneNobetDegisimler { get; set; }
        public virtual List<Kalibrasyon> Kalibrasyonlar { get; set; }
        public virtual List<EczaneNobetFeragat> EczaneNobetFeragatlar { get; set; }
        public virtual List<DebugEczane> DebugEczaneler { get; set; }
    }
}
