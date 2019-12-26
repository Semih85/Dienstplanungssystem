using OPTANO.Modeling.Optimization;
using System.Collections.Generic;
using WM.Core.Optimization.Optano;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.Optimization.Entities.KisitParametre
{
    public class KpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu : OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>
    {
        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralik { get; set; }
        public List<EczaneNobetTarihAralikIkili> EczaneNobetTarihAralikIkiliEczaneler { get; set; }
        public List<AyniGunTutulanNobetDetay> IkiliEczaneler { get; set; }
        public NobetUstGrupKisitDetay NobetUstGrupKisit { get; set; }
        public List<TakvimNobetGrup> Tarihler { get; set; }
        public VariableCollection<EczaneNobetTarihAralikIkili> KararDegiskeniIkiliEczaneler { get; set; }
        public List<EczaneGrupDetay> EczaneGruplar { get; internal set; }
        public List<NobetGrupKuralDetay> NobetGrupKurallar { get; internal set; }

        public override OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik> Clone()
        {
            return (OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>)MemberwiseClone();
        }
    }
}

/*
    Model model,
    
    List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
    List<EczaneNobetTarihAralikIkili> eczaneNobetTarihAralikIkiliEczaneler,
    List<AyniGunTutulanNobetDetay> ikiliEczaneler,
    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
    List<TakvimNobetGrup> tarihler,
    
    VariableCollection<EczaneNobetTarihAralik> _x
    VariableCollection<EczaneNobetTarihAralikIkili> _y
 */
