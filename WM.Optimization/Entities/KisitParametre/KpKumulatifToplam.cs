using OPTANO.Modeling.Optimization;
using System.Collections.Generic;
using WM.Core.Optimization.Optano;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.Optimization.Entities.KisitParametre
{
    public class KpKumulatifToplam : OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>
    {
        public List<TakvimNobetGrup> Tarihler { get; set; }
        public EczaneNobetGrupDetay EczaneNobetGrup { get; set; }
        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralik { get; set; }
        public NobetUstGrupKisitDetay NobetUstGrupKisit { get; set; }
        public double KumulatifOrtalamaNobetSayisi { get; set; }
        public int ToplamNobetSayisi { get; set; }
        public bool EnAzMi { get; set; }
        public string GunKuralAdi { get; set; }

        public override OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik> Clone()
        {
            return (OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>)MemberwiseClone();
        }
    }
}

/*
    Model model,
    List<TakvimNobetGrup> tarihler,
    EczaneNobetGrupDetay eczaneNobetGrup,
    List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
    double kumulatifOrtalamaGunKuralSayisi,
    int toplamNobetSayisi,
    VariableCollection<EczaneNobetTarihAralik> _x
 */
