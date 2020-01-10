using OPTANO.Modeling.Optimization;
using System.Collections.Generic;
using WM.Core.Optimization.Optano;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.Optimization.Entities.KisitParametre
{
    public class KpTarihVeAltGrupBazliEnFazla : OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>
    {
        public TakvimNobetGrup Tarih { get; set; }
        public int GunSayisi { get; set; }
        public double OrtalamaNobetSayisi { get; set; }
        public string GunKuralAdi { get; set; }
        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralik { get; set; }
        public NobetUstGrupKisitDetay NobetUstGrupKisit { get; set; }

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
