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
    public class NobetUstGrupKisitCoklu : IComplexType
    {
        public int Id { get; set; }
        public int NobetUstGrupId { get; set; }
        public int[] KisitId { get; set; }
        public double SagTarafDegeri { get; set; }
        public double SagTarafDegeriVarsayilan { get; set; }
        public bool PasifMi { get; set; }
        public bool VarsayilanPasifMi { get; set; }
        public string Aciklama { get; set; }
    }
}