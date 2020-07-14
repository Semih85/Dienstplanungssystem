using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class EczaneNobetGrupKisit : IEntity
    {
        public int Id { get; set; }
        public int NobetUstGrupKisitId { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public bool PasifMi { get; set; }
        public double SagTarafDegeri { get; set; }
        public bool VarsayilanPasifMi { get; set; }
        public double SagTarafDegeriVarsayilan { get; set; }
        public string Aciklama { get; set; }
        public virtual EczaneNobetGrup EczaneNobetGrup { get; set; }
        public virtual NobetUstGrupKisit NobetUstGrupKisit { get; set; }

    }
}