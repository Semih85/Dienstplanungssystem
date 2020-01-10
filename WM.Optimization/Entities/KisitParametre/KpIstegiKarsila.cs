using OPTANO.Modeling.Optimization;
using System.Collections.Generic;
using WM.Core.Optimization.Optano;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.Optimization.Entities.KisitParametre
{
    public class KpIstegiKarsila : OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>
    {
        public List<TakvimNobetGrup> Tarihler { get; set; }
        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralik { get; set; }
        public List<EczaneNobetIstekDetay> EczaneNobetIstekler { get; set; }
        public NobetUstGrupKisitDetay NobetUstGrupKisit { get; set; }

        public override OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik> Clone()
        {
            return (OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>)MemberwiseClone();
        }
    }
}

/*
     Model model,
     List<EczaneNobetTarihAralik> eczaneNobetTarihAralikTumu,
     NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
     List<EczaneNobetIstekDetay> eczaneNobetIstekler,
     VariableCollection<EczaneNobetTarihAralik> _x
 */
