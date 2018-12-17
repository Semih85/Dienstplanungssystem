using OPTANO.Modeling.Optimization;
using System.Collections.Generic;
using WM.Core.Optimization.Optano;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.Optimization.Entities.KisitParametre
{
    public class KpHerAyPespeseGorev : OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>
    {
        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralik { get; set; }
        public NobetUstGrupKisitDetay NobetUstGrupKisit { get; set; }
        public List<TakvimNobetGrup> Tarihler { get; set; }
        public int PespeseNobetSayisi { get; set; }
        public EczaneNobetGrupDetay EczaneNobetGrup { get; set; }

        public override OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik> Clone()
        {
            return (OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>)MemberwiseClone();
        }
    }
}

/*
     Model model,
     List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
     NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
     List<TakvimNobetGrup> tarihler,
     int pespeseNobetSayisi,
     EczaneNobetGrupDetay eczaneNobetGrup,
     VariableCollection<EczaneNobetTarihAralik> _x
 */
