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
    public class EczaneNobetTekGrupDataModel: IDataModel
    {
        public int Yil { get; set; }
        public int Ay { get; set; }
        
        public int LowerBound { get; set; }
        public int UpperBound { get; set; }
        
        public List<EczaneNobetMazeretDetay> EczaneNobetMazeretListe { get; set; }
        public List<EczaneNobetIstekDetay> EczaneNobetIstekler { get; set; }
        public List<EczaneNobetTarihAralik> EczaneNobetTarihAralik { get; set; }
        public List<EczaneNobetIstatistik> EczaneKumulatifHedefler { get; set; }
        public List<NobetGrupGunKural> NobetGrupGunKurallar { get; set; }
        public List<NobetGrupTalepDetay> NobetGrupTalepler { get; set; }
        public List<NobetGrupGorevTip> NobetGrupGorevTipler { get; set; }
        //public List<TarihAraligi> TarihAraligi { get; set; }
        public List<TakvimNobetGrup> TarihAraligi { get; set; }
        public NobetGrup NobetGrup { get; set; }
        public int PespeseNobet { get; set; }
        public int GerekliNobetSayisi { get; set; }
    }
}
