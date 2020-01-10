using OPTANO.Modeling.Optimization;
using System.Collections.Generic;
using WM.Core.Optimization.Optano;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.Optimization.Entities.KisitParametre
{
    public class KpBase : OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>
    {
        public override OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik> Clone()
        {
            return (OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>)MemberwiseClone();
        }
    }
}

/*
    Model model,
    List<TakvimNobetGrup> tarihler,
    int gunSayisi,
    double ortalamaNobetSayisi,
    EczaneNobetGrupDetay eczaneNobetGrup,
    List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
    VariableCollection<EczaneNobetTarihAralik> _x
 */
