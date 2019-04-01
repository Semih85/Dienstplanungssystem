using OPTANO.Modeling.Optimization;
using System;
using System.Collections.Generic;
using WM.Core.Optimization.Optano;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.Optimization.Entities.KisitParametre
{
    public class KpAltGruplarlaAyniGunNobetGrupAltGrup : OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>
    {
        public List<AltGrupIleTutulanNobetDurumu> AltGrupIleTutulanNobetDurumlari { get; set; }
        public List<NobetGrupGorevTipDetay> NobetGrupGorevTipler { get; set; }
        public NobetUstGrupKisitDetay NobetUstGrupKisit { get; set; }
        public List<EczaneNobetGrupDetay> EczaneNobetGruplar { get; set; }
        public List<TakvimNobetGrup> Tarihler { get; set; }
        public List<AyniGunNobetTakipGrupAltGrupDetay> AyniGunNobetTakipGrupAltGruplar { get; set; }
        public List<EczaneNobetSonucListe2> EczaneNobetSonuclar { get; set; }
        public List<EczaneNobetGrupAltGrupDetay> EczaneNobetGrupAltGruplar { get; set; }

        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralik { get; set; }
        public List<EczaneNobetAltGrupTarihAralik> EczaneNobetAltGrupTarihAralik { get; set; }

        public VariableCollection<EczaneNobetAltGrupTarihAralik> KararDegiskeni2 { get; set; }

        public override OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik> Clone()
        {
            return (OptanoKisitParametreModelBase<Model, EczaneNobetTarihAralik>)MemberwiseClone();
        }
    }
}

