using OPTANO.Modeling.Optimization;
using System.Collections.Generic;
using WM.Core.Optimization.Optano;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.Optimization.Entities.KisitParametre
{
    public class KpTalebiKarsila : OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>
    {
        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralikTumu { get; set; }
        public NobetGrupGorevTipDetay NobetGrupGorevTip { get; set; }
        public List<TakvimNobetGrup> Tarihler { get; set; }
        public NobetUstGrupKisitDetay NobetUstGrupKisit { get; internal set; }
        public string TalepDetay { get; set; }

        public override OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik> Clone()
        {
            return (OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>)MemberwiseClone();
        }
    }
}

/*
    //public int GunlukNobetciSayisi { get; set; }
    //public List<NobetGrupTalepDetay> NobetGrupTalepler { get; set; }

    Model model,
    List<EczaneNobetTarihAralik> eczaneNobetTarihAralikTumu,
    int gunlukNobetciSayisi,
    List<NobetGrupTalepDetay> nobetGrupTalepler,
    NobetGrupGorevTipDetay nobetGrupGorevTip,
    List<TakvimNobetGrup> tarihler,
    VariableCollection<EczaneNobetTarihAralik> _x
 */
