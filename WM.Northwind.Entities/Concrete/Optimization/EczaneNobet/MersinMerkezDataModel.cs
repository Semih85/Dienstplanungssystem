using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Optimization;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.Concrete.Optimization.EczaneNobet
{
    public class MersinMerkezDataModel : IDataModel
    {
        public MersinMerkezDataModel()
        {
            CozumItereasyon = new CozumItereasyon();
        }

        public int Yil { get; set; }
        public int Ay { get; set; }
        public int NobetUstGrupId { get; set; }

        public int LowerBound { get; set; }
        public int UpperBound { get; set; }
        public int CalismaSayisi { get; set; }
        public DateTime NobetUstGrupBaslangicTarihi { get; set; }

        public List<EczaneGrupDetay> EczaneGruplar { get; set; }
        public List<EczaneGrupTanim> EczaneGrupTanimlar { get; set; }
        public List<EczaneNobetIstatistik> EczaneKumulatifHedefler { get; set; }

        public List<EczaneNobetMazeretDetay> EczaneNobetMazeretler { get; set; }
        public List<EczaneNobetIstekDetay> EczaneNobetIstekler { get; set; }
        
        public List<TakvimNobetGrup> TarihAraligi { get; set; }
        public List<NobetGrup> NobetGruplar { get; set; }
        public List<NobetGrupKuralDetay> NobetGrupKurallar { get; set; }
        public List<NobetGrupGunKural> NobetGrupGunKurallar { get; set; }
        public List<NobetGrupGorevTip> NobetGrupGorevTipler { get; set; }
        public List<NobetGrupTalepDetay> NobetGrupTalepler { get; set; }
        public List<EczaneNobetGrupDetay> EczaneNobetGruplar { get; set; }
        public List<NobetUstGrupKisitDetay> NobetUstGrupKisitlar { get; set; }
        public List<EczaneGrupNobetSonuc> EczaneGrupNobetSonuclar { get; set; }
        public List<EczaneGrupNobetSonuc> EczaneGrupNobetSonuclarTumu { get; set; }
        public List<EczaneGrupNobetSonuc> EczaneNobetSonuclar { get; set; }
        public List<EczaneNobetGrupGunKuralIstatistik> EczaneNobetGrupGunKuralIstatistikler { get; set; }
        public List<TakvimNobetGrupGunDegerIstatistik> TakvimNobetGrupGunDegerIstatistikler { get; set; }

        public CozumItereasyon CozumItereasyon { get; set; }

        //birlikte nöbet için kullanılan değişkenler
        //public List<EczaneCiftGrup> AyIcindeAyniGunNobetTutanEczaneler { get; set; }
        //public List<EczaneCiftGrup> SonIkiAyAyniGunNobetTutanEczaneler { get; set; }
        //public List<EczaneCiftGrup> YilIcindeAyniGunNobetTutanEczaneler { get; set; }
        //public List<EczaneNobetSonucListe> EczaneNobetSonuclarOncekiAylar { get; set; }
        //public List<EczaneNobetSonucListe> EczaneNobetSonuclarSonIkiAy { get; set; }

        //Karar Değişkeni
        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralik { get; set; }
    }
}
