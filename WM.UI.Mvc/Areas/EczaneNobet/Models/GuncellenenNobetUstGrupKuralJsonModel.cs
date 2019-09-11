using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.UI.Mvc
{
    public class GuncellenenNobetUstGrupKuralJsonModel
    {
        public string Mesaj { get; set; }
        public bool VarsayilandanFarkliMi { get; set; }
        public int DegisenKisitSayisi { get; set; }
        public int GrupBazliKisitSayisi { get; internal set; }
        public double SagTarafDegeri { get; internal set; }
        public double SagTarafDegeriVarsayilan { get; internal set; }
        public bool PasifMi { get; internal set; }
        public bool PasifMiVarsayilan { get; internal set; }
    }
}