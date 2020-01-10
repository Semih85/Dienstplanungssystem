using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.Concrete.Optimization.EczaneNobet
{
    public class EczaneNobetSonucModel
    {
        public EczaneNobetSonucModel()
        {
            CozumSuresi = ToplamSure = new TimeSpan();
        }

        public List<EczaneNobetCozum> ResultModel { get; set; }
        public double ObjectiveValue { get; set; }
        public TimeSpan CozumSuresi { get; set; }
        public TimeSpan ToplamSure { get; set; }
        public int KisitSayisi { get; set; }
        public long BakilanDugumSayisi { get; set; }
        public int KararDegikeniSayisi { get; set; }
        public int IterasyonSayisi { get; set; }
        public int CalismaSayisi { get; set; }
        public int NobetGrupSayisi { get; set; }
        public int IncelenenEczaneSayisi { get; set; }
        public string Celiskiler { get; set; }
        //public int NobetYazilamayanEczaneSayisi { get; set; }
    }
}
