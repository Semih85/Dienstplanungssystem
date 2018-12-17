using OPTANO.Modeling.Optimization;
using System.Collections.Generic;
using WM.Core.Optimization.Optano;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.Optimization.Entities.KisitParametre
{
    public class KpMazereteGorevYazma : OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>
    {
        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralik { get; set; }
        public NobetUstGrupKisitDetay NobetUstGrupKisit { get; set; }
        public List<EczaneNobetMazeretDetay> EczaneNobetMazeretler { get; set; }

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
     List<EczaneNobetMazeretDetay> eczaneNobetMazeretler,
     VariableCollection<EczaneNobetTarihAralik> _x
 */
