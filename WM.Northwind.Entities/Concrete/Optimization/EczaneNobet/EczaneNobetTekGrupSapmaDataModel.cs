using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.Concrete.Optimization.EczaneNobet
{
    public class EczaneNobetTekGrupSapmaDataModel
    {
        public int Yil { get; set; }
        public int Ay { get; set; }
        //public int NobetGrupId { get; set; }

        public int LowerBound { get; set; }
        public int UpperBound { get; set; }
        
        public List<EczaneNobetMazeretDetay> EczaneNobetMazeretListe { get; set; }
        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralik { get; set; }
        public List<EczaneNobetIstatistik> EczaneKumulatifHedefler { get; set; }
        public List<TarihAraligi> TarihAraligi { get; set; }
        public NobetGrup NobetGrup { get; set; }

        //public List<EczaneNobetGrup> EczaneNobetGruplar { get; set; }
        //public List<EczaneNobetIstatistik> EczaneNobetIstatistikler { get; set; }
        //public List<EczaneNobetIstatistik2> EczaneKumulatifHedefler2 { get; set; }
        //public List<GrupIciAylikKumulatifHedef> NobetGrupKumulatifHedefler { get; set; }
        //public List<NobetTalep> NobetTalepler { get; set; }
        //public List<EczaneNobetSonucAktif> EczaneNobetSonucAktifler { get; set; }
    }
}
