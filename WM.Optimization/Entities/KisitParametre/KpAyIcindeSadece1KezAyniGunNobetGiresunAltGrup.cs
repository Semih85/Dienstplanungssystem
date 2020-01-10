using OPTANO.Modeling.Optimization;
using System;
using System.Collections.Generic;
using WM.Core.Optimization.Optano;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.Optimization.Entities.KisitParametre
{
    public class KpAyIcindeSadece1KezAyniGunNobetGiresunAltGrup : OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>
    {
        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralik { get; set; }
        public List<EczaneNobetGrupAltGrupDetay> EczaneNobetGrupAltGrupDetaylar { get; set; }
        public NobetUstGrupKisitDetay NobetUstGrupKisit { get; set; }
        public List<TakvimNobetGrup> Tarihler { get; set; }

        public override OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik> Clone()
        {
            return (OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>)MemberwiseClone();
        }
    }
}

/*
    Model model,
    List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
    List<AyniGunTutulanNobetDetay> ikiliEczaneler,
    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
    List<TakvimNobetGrup> tarihler,
    VariableCollection<EczaneNobetTarihAralik> _x
 */
