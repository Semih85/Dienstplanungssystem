using OPTANO.Modeling.Optimization;
using System.Collections.Generic;
using WM.Core.Optimization.Optano;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.Optimization.Entities.KisitParametre
{
    public class KpEsGrubaAyniGunNobetYazma : OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>
    {
        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralik { get; set; }
        public List<EczaneNobetSonucListe2> EczaneNobetSonuclar { get; set; }
        public List<TakvimNobetGrup> Tarihler { get; set; }
        public List<EczaneGrupDetay> EczaneGruplar { get; set; }
        public NobetUstGrupKisitDetay NobetUstGrupKisit { get; set; }
        public string NobetGrupGorevTipAdi { get; set; }

        public override OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik> Clone()
        {
            return (OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>)MemberwiseClone();
        }
    }
}

/*
    Model model,
    List<EczaneNobetTarihAralik> eczaneNobetTarihAralikTumu,
    List<EczaneNobetSonucListe2> eczaneNobetSonuclarTumu,
    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
    List<EczaneGrupDetay> eczaneGruplarTumu,
    List<TakvimNobetGrup> tarihlerTumu,
    VariableCollection<EczaneNobetTarihAralik> _x
 */
