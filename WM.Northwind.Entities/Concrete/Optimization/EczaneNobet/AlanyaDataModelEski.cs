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
    public class AlanyaDataModelEski : IDataModel
    {
        public AlanyaDataModelEski()
        {
            CozumItereasyon = new CozumItereasyon();
        }

        public int Yil { get; set; }
        public int Ay { get; set; }
        public int NobetUstGrupId { get; set; }

        public int LowerBound { get; set; }
        public int UpperBound { get; set; }
        public int CalismaSayisi { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }

        public CozumItereasyon CozumItereasyon { get; set; }
        public List<EczaneGrupDetay> EczaneGruplar { get; set; }
        public List<EczaneGrupTanim> EczaneGrupTanimlar { get; set; }
        public List<EczaneNobetGrup> EczaneNobetGruplar { get; set; }
        public List<EczaneNobetIstatistik> EczaneKumulatifHedefler { get; set; }
        public List<EczaneNobetIstekDetay> EczaneNobetIstekListe { get; set; }
        public List<EczaneNobetMazeretDetay> EczaneNobetMazeretListe { get; set; }
        public List<EczaneNobetSonucListe2> EczaneGrupNobetSonuclar { get; set; }
        public List<EczaneNobetSonucListe2> EczaneNobetSonuclar { get; set; }

        #region birlikte nöbet için kullanılan değişkenler
        public List<EczaneCiftGrup> AyIcindeAyniGunNobetTutanEczaneler { get; set; }
        public List<EczaneCiftGrup> SonIkiAyAyniGunNobetTutanEczaneler { get; set; }
        public List<EczaneCiftGrup> YilIcindeAyniGunNobetTutanEczaneler { get; set; }
        public List<EczaneNobetSonucListe> EczaneNobetSonuclarOncekiAylar { get; set; }
        public List<EczaneNobetSonucListe> EczaneNobetSonuclarSonIkiAy { get; set; } 
        #endregion

        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralik { get; set; } //Karar Değişkeni
        public List<NobetGrup> NobetGruplar { get; set; }
        public List<NobetGrupGorevTip> NobetGrupGorevTipler { get; set; }
        public List<NobetGrupGunKural> NobetGrupGunKurallar { get; set; }
        public List<NobetGrupKuralDetay> NobetGrupKurallar { get; set; }
        public List<NobetGrupTalepDetay> NobetGrupTalepler { get; set; }
        public List<NobetUstGrupKisitDetay> NobetUstGrupKisitlar { get; set; }
        public List<TakvimNobetGrup> TarihAraligi { get; set; }
    }
}


