using OPTANO.Modeling.Optimization;
using System;
using System.Collections.Generic;
using WM.Core.Optimization.Optano;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.Optimization.Entities.KisitParametre
{
    public class KpPesPeseGorevEnAz : OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>
    {
        public KpPesPeseGorevEnAz()
        {
            EczaneNobetIstekler = new List<EczaneNobetIstekDetay>();
        }
        public int NobetSayisi { get; set; }
        public List<TakvimNobetGrup> Tarihler { get; set; }
        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralik { get; set; }
        public List<EczaneNobetIstekDetay> EczaneNobetIstekler { get; set; }
        public DateTime NobetYazilabilecekIlkTarih { get; set; }
        public DateTime SonNobetTarihi { get; set; }
        public EczaneNobetGrupDetay EczaneNobetGrup { get; set; }
        public NobetUstGrupKisitDetay NobetUstGrupKisit { get; set; }

        public override OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik> Clone()
        {
            return (OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>)MemberwiseClone();
        }
    }
}

/*
    Model model,
    int nobetSayisi,
    List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
    List<TakvimNobetGrup> tarihler,
    DateTime nobetYazilabilecekIlkTarih,
    EczaneNobetGrupDetay eczaneNobetGrup,
    VariableCollection<EczaneNobetTarihAralik> _x
 */
