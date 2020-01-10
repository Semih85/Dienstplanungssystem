using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetMazeretIstekDetay : IComplexType
    {
        public int Id { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int NobetUstGrupId { get; set; }
        public int NobetGrupId { get; set; }
        public int EczaneId { get; set; }
        public int MazeretIstekId { get; set; }//1 mazeret, 2 istek
        public string MazeretIstekTipAdi => MazeretIstekId == 1 ? "Mazeret" : "İstek";

        public int TakvimId { get; set; }
        public int Yil { get; set; }
        public int Ay { get; set; }
        public int Gun { get; set; }

        [Display(Name = "Grup")]
        public string NobetGrupAdi { get; set; }
        public string NobetGorevTipAdi { get; set; }
        [Display(Name = "Eczane")]
        public string EczaneAdi { get; set; }
        [Display(Name = "Mazeret")]
        public string MazeretIstekAdi { get; set; }
        public int MazeretIstekTurId { get; set; }
        public string MazeretIstekTuru { get; set; }
        public DateTime Tarih { get; set; }
        public string TarihKisa => Tarih.ToShortDateString();
        public string TarihAciklama => String.Format("{0:d MMM yyyy, ddd}", Tarih);

        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }

        public DateTime? BaslangicTarihi { get; internal set; }
        public DateTime? BitisTarihi { get; internal set; }
    }
}
