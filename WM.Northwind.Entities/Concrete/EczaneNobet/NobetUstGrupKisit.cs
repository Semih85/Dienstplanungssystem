using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class NobetUstGrupKisit : IEntity
    {
        public int Id { get; set; }
        public int NobetUstGrupId { get; set; }
        public int KisitId { get; set; }
        public double SagTarafDegeri { get; set; }
        public double SagTarafDegeriVarsayilan { get; set; }
        public bool PasifMi { get; set; }
        public bool VarsayilanPasifMi { get; set; }
        public string Aciklama { get; set; }

        public virtual Kisit Kisit { get; set; }
        public virtual NobetUstGrup NobetUstGrup { get; set; }
        public virtual List<NobetGrupGorevTipKisit> NobetGrupGorevTipKisitlar { get; set; }
    }
}