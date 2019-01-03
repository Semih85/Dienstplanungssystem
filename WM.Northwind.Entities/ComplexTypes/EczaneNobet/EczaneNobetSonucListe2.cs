using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetSonucListe2
    {
        public int Id { get; set; }
        [Display(Name = "Eczane")]
        public string EczaneAdi { get; set; }
        [Display(Name = "Nöbet Grubu")]
        public string NobetGrupAdi { get; set; }
        public string NobetGrubu => $"{NobetGrupId} {NobetGrupAdi}";
        //NobetUstGrupId == 3 
        //? $"{NobetGrupId} {NobetGrupAdi}"
        //: $"{NobetGrupAdi}";

        public int EczaneNobetGrupId { get; set; }
        public int NobetUstGrupId { get; set; }
        public int NobetAltGrupId { get; set; }
        public string NobetAltGrupAdi { get; set; }

        public int NobetGrupId { get; set; }
        public int EczaneId { get; set; }
        public int MazeretId { get; set; }
        public int IstekId { get; set; }
        public string MazeretTuru { get; set; }
        public string Mazeret { get; set; }
        public int NobetSonucDemoTipId { get; set; }
        [Display(Name = "Görev Tipi")]
        public string NobetGorevTipAdi { get; set; }
        public int NobetGorevTipId { get; set; }
        [Display(Name = "Gün")]
        public string GunTanim { get; set; }
        [Display(Name = "Gün Değer")]
        public int NobetGunKuralId { get; set; }
        [Display(Name = "Gün Grup")]
        public string GunGrup { get; set; }
        public int GunGrupId { get; set; }
        [Display(Name = "Ayın Günü")]
        public int Gun { get; set; }
        [Display(Name = "Yıl")]
        public int Yil { get; set; }
        public int Ay { get; set; }
        public int TakvimId { get; set; }
        public DateTime Tarih { get; set; }
        public DateTime EczaneNobetGrupBaslamaTarihi { get; set; }
        public DateTime NobetUstGrupBaslamaTarihi { get; set; }
        public string TarihAciklama => String.Format("{0:d MMM yyyy, ddd}", Tarih);
        public string Tarih2 => String.Format("{0:yyyy MM dd}", Tarih);
        public string Yıl_Ay => String.Format("{0:yy MM}", Tarih);
        public string GunIkiHane => String.Format("{0:dd}", Tarih);
        public int NobetTipId => MazeretId > 0
            ? 1
            : IstekId > 0
            ? 2
            : 0;
        public string NobetTipi => MazeretId > 0
            ? "Mazerete Yazılan Nöbet"
            : IstekId > 0
            ? "İstek Nöbeti"
            : "Normal Nöbet";

        public string EczaneSonucAdi => GetEczaneSonuc(MazeretId, IstekId, EczaneAdi);
        public string AyIkili => GetIkiliAylar(Ay);
        public string Mevsim => GetMevsim(Ay);

        public string SonucTuru { get; set; }
        public int NobetGrupGorevTipId { get; set; }

        string GetEczaneSonuc(int mazeretId, int istekId, string eczaneAdi)
        {
            if (mazeretId > 0)
            {
                return $"{eczaneAdi}.m";
            }
            else if (istekId > 0)
            {
                return $"{eczaneAdi}.i";
            }
            else
            {
                return eczaneAdi;
            }
        }

        string GetIkiliAylar(int ay)
        {
            if (ay <= 2)
            {
                return "1-2";
            }
            else if (ay <= 4)
            {
                return "3-4";
            }
            else if (ay <= 6)
            {
                return "5-6";
            }
            else if (ay <= 8)
            {
                return "7-8";
            }
            else if (ay <= 10)
            {
                return "9-10";
            }
            else
            {
                return "11-12";
            }
        }

        string GetMevsim(int ay)
        {
            if (ay <= 2)
            {
                return "Kış";
            }
            else if (ay <= 5)
            {
                return "İlkbahar";
            }
            else if (ay <= 8)
            {
                return "Yaz";
            }
            else if (ay <= 11)
            {
                return "Sonbahar";
            }
            else
            {
                return "Kış";
            }
        }
    }


}
