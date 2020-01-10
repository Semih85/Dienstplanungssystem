using OPTANO.Modeling.Optimization;
using System.Collections.Generic;
using WM.Core.Optimization.Optano;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.Optimization.Entities.KisitParametre
{
    public class KpTarihAraligindaEnAz1NobetYaz : OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>
    {
        public NobetUstGrupKisitDetay NobetUstGrupKisit { get; set; }
        public List<TakvimNobetGrup> Tarihler { get; set; }
        public int GruptakiNobetciSayisi { get; set; }
        public EczaneNobetGrupDetay EczaneNobetGrup { get; set; }
        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralik { get; set; }
        //public int GunlukNobetciSayisi { get; internal set; }
        public int IstisnaOlanNobetciSayisi { get; internal set; }
        public string KuralAciklama { get; set; }

        public override OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik> Clone()
        {
            return (OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>)MemberwiseClone();
        }
    }
}

/*
    Model model,
    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
    List<TakvimNobetGrup> tarihler,
    int gruptakiNobetciSayisi,
    EczaneNobetGrupDetay eczaneNobetGrup,
    List<EczaneNobetTarihAralik> eczaneNobetTarihAralikEczaneBazli,
    VariableCollection<EczaneNobetTarihAralik> _x
 */
