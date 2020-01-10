using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class Eczane : Iletisim, IEntity
    {
        public Eczane()
        {

        }

        public Eczane(int nobetUstGrupId)
        {
            NobetUstGrupId = nobetUstGrupId;
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Adı")]
        public string Adi { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Açılış Tarihi")]
        public DateTime AcilisTarihi { get; set; }
        [Display(Name = "Kapanış Tarihi")]
        public DateTime? KapanisTarihi { get; set; }
        [Display(Name = "Adres Tarifi")]
        public string AdresTarifi { get; set; }
        [Display(Name = "Adres Tarifi Kısa")]
        public string AdresTarifiKisa { get; set; }
        public double Enlem { get; set; }
        public double Boylam { get; set; }
        public int NobetUstGrupId { get; set; }

        public virtual List<EczaneNobetMuafiyet> EczaneNobetMuafiyetler { get; set; }
        public virtual List<EczaneNobetGrup> EczaneNobetGruplar { get; set; }
        public virtual List<EczaneGrup> EczaneGruplar { get; set; }
        public virtual List<UserEczane> UserEczaneler { get; set; }
        public virtual List<EczaneIlce> EczaneIlceler { get; set; }
        public virtual List<EczaneGorevTip> EczaneGorevTipler { get; set; }
        public virtual NobetUstGrup NobetUstGrup { get; set; }
        public virtual List<EczaneUzaklikMatris> EczaneUzaklikMatrislerFrom { get; set; }
        public virtual List<EczaneUzaklikMatris> EczaneUzaklikMatrislerTo { get; set; }

    }
}
