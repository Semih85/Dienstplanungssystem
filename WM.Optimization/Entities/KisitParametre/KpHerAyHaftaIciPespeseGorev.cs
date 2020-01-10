using OPTANO.Modeling.Optimization;
using System.Collections.Generic;
using WM.Core.Optimization.Optano;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.Optimization.Entities.KisitParametre
{
    public class KpHerAyHaftaIciPespeseGorev : OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>
    {
        //public List<TakvimNobetGrup> Tarihler { get; set; }
        public List<TakvimNobetGrup> Tarihler { get; set; }
        public double OrtamalaNobetSayisi { get; set; }
        //public int GunlukNobetciSayisi { get; set; }
        public string GunKuralAdi { get; set; }
        public EczaneNobetGrupDetay EczaneNobetGrup { get; set; }
        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralik { get; set; }
        public double PespeseNobetSayisiAltLimit { get; set; }
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
    List<TakvimNobetGrup> haftaIciGunleri,
    double haftaIciOrtamalaNobetSayisi,
    EczaneNobetGrupDetay eczaneNobetGrup,
    List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
    double pespeseNobetSayisiAltLimit,
    bool kisitAktifMi
    VariableCollection<EczaneNobetTarihAralik> _x
 */
