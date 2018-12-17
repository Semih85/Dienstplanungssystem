using OPTANO.Modeling.Optimization;
using System;
using System.Collections.Generic;
using WM.Core.Optimization.Optano;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.Optimization.Entities.KisitParametre
{
    public class KpAyniGunSadece1NobetTuru : OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>
    {
        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralik { get; set; }
        public List<EczaneNobetGrupDetay> EczaneNobetGruplar { get; set; }
        public List<TakvimNobetGrup> Tarihler { get; set; }
        public NobetUstGrupKisitDetay NobetUstGrupKisit { get; set; }

        public override OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik> Clone()
        {
            return (OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>)MemberwiseClone();
        }
    }
}

/*
    Model model,
    List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
    List<EczaneNobetGrupDetay> eczaneNobetGruplar,
    List<TakvimNobetGrup> tarihler,
    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
    VariableCollection<EczaneNobetTarihAralik> _x
 */
