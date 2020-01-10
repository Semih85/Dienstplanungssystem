using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class NobetUstGrup : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Adı")]
        public string Adi { get; set; }
        [Display(Name = "Eczane Oda Id")]
        public int EczaneOdaId { get; set; }
        [Display(Name = "Başlangıç Tarihi")]
        public DateTime BaslangicTarihi { get; set; }
        //Bitiş tarihi null ise, bu eczane o grupta aktiftir.
        [Display(Name = "Bitiş Tarihi")]
        public DateTime? BitisTarihi { get; set; }
        //Aciklamada nöbet grubu değişikliğinin gerekçesi belirtilecektir.
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }

        public int TimeLimit { get; set; }
        public double Enlem { get; set; }
        public double Boylam { get; set; }

        public int OneedeGosterilecekEnUzakMesafe { get; set; }
        public bool BaslamaTarihindenOncekiSonuclarGosterilsinMi { get; set; }

        public virtual EczaneOda EczaneOda { get; set; }
        public virtual List<NobetGrup> NobetGruplar { get; set; }
        public virtual List<EczaneGrupTanim> EczaneGrupTanimlar { get; set; }
        public virtual List<UserNobetUstGrup> UserNobetUstGruplar { get; set; }
        public virtual List<NobetUstGrupKisit> NobetUstGrupKisitlar { get; set; }
        public virtual List<NobetUstGrupGunGrup> NobetUstGrupGunGruplar { get; set; }
        public virtual List<Eczane> Eczaneler { get; set; }
        public virtual List<KalibrasyonTip> KalibrasyonTipler { get; set; }
        public virtual List<RaporNobetUstGrup>  RaporNobetUstGruplar { get; set; }
        //public virtual List<AyniGunTutulanNobet> AyniGunTutulanNobetler { get; set; }

    }
}
