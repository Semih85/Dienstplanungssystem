using OPTANO.Modeling.Optimization;
using System.Collections.Generic;
using WM.Core.Optimization.Optano;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.Optimization.Entities.KisitParametre
{
    public class KpPespeseFarkliTurNobet : OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>
    {
        public List<TakvimNobetGrup> Tarihler { get; set; }
        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralik { get; set; }
        public EczaneNobetGrupDetay EczaneNobetGrup { get; set; }
        public EczaneNobetSonucListe2 SonNobet { get; set; }
        public NobetUstGrupKisitDetay NobetUstGrupKisit { get; set; }

        public override OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik> Clone()
        {
            return (OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>)MemberwiseClone();
        }
    }
}

/*
    Model model,
    List<TakvimNobetGrup> bayramlar,
    List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
    EczaneNobetGrupDetay eczaneNobetGrup,
    int sonBayramTuru,
    VariableCollection<EczaneNobetTarihAralik> _x
 */
