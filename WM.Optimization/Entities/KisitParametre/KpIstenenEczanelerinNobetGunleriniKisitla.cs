using OPTANO.Modeling.Optimization;
using System.Collections.Generic;
using WM.Core.Optimization.Optano;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.Optimization.Entities.KisitParametre
{
    public class KpIstenenEczanelerinNobetGunleriniKisitla : OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>
    {
        //public List<int> NobetYazilmayacakGunKuralIdList { get; set; }
        public List<EczaneNobetTutamazGun> EczaneNobetTutamazGunler { get; set; }
        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralik { get; set; }
        public NobetUstGrupKisitDetay NobetUstGrupKisit { get; set; }

        public override OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik> Clone()
        {
            return (OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>)MemberwiseClone();
        }
    }
}

/*
    Model model,
    List<int> nobetYazilmayacakGunKuralIdList,
    List<EczaneNobetGrupDetay> eczaneNobetGruplar,
    List<EczaneNobetTarihAralik> eczaneNobetTarihAralikTumu,
    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
    VariableCollection<EczaneNobetTarihAralik> _x
 */
