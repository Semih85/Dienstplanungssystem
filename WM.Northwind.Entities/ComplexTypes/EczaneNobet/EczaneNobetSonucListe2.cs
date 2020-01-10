using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Enums;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetSonucListe2: ICloneable
    {
        public EczaneNobetSonucListe2()
        {

        }

        public EczaneNobetSonucListe2(EczaneNobetSonucTuru sonucTuru)
        {
            SonucTuru = sonucTuru;
        }
        public int Id { get; set; }
        [Display(Name = "Eczane")]
        public string EczaneAdi { get; set; }
        [Display(Name = "Nöbet Grubu")]
        public string NobetGrupAdi { get; set; }
        public string NobetGrupAdiGunluk { get; set; }
        public string NobetGrubu => $"{NobetGrupGorevTipId} {NobetGrupAdi}";
        //NobetUstGrupId == 3 
        //? $"{NobetGrupId} {NobetGrupAdi}"
        //: $"{NobetGrupAdi}";
        public bool YayimlandiMi { get; set; }
        public bool SanalNobetMi { get; set; }
        public double AgirlikDegeri { get; set; }
        public string NobetOzelGunAdi { get; set; }
        public string NobetOzelGunKategoriAdi { get; set; }

        public int EczaneNobetGrupId { get; set; }
        public int NobetUstGrupId { get; set; }
        public int NobetAltGrupId { get; set; }
        public int NobetDurumId { get; set; }
        public int NobetDurumTipId { get; set; }
        public string NobetDurumAdi { get; set; }
        public string NobetDurumTipAdi { get; set; }

        public string NobetAltGrupAdi { get; set; }

        public int NobetSayisi { get; set; }
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
        public string NobetGunKuralAdi { get; set; }
        [Display(Name = "Gün Değer")]
        public int NobetGunKuralId { get; set; }
        [Display(Name = "Gün Grup")]
        public string GunGrupAdi { get; set; }
        public int GunGrupId { get; set; }
        [Display(Name = "Ayın Günü")]
        public int Gun { get; set; }
        [Display(Name = "Yıl")]
        public int Yil { get; set; }
        public int Ay { get; set; }
        public int TakvimId { get; set; }
        public int Hafta => GetIso8601WeekOfYear(Tarih);

        public DateTime Tarih { get; set; }
        public DateTime EczaneNobetGrupBaslamaTarihi { get; set; }
        public string GrubaKatilisTarihi => String.Format("{0:yyyy.MM.dd}", EczaneNobetGrupBaslamaTarihi);
        public DateTime? EczaneNobetGrupBitisTarihi { get; set; }
        public string GruptanAyrilisTarihi => String.Format("{0:yyyy.MM.dd}", EczaneNobetGrupBitisTarihi);
        public DateTime NobetUstGrupBaslamaTarihi { get; set; }
        public DateTime NobetGrupGorevTipBaslamaTarihi { get; set; }
        public string TarihAciklama => $"{String.Format("{0:d MMM yyyy, ddd}", Tarih)} {(SanalNobetMi ? "Sanal" : "")}";
        public string Tarih2 => String.Format("{0:yyyy.MM.dd}", Tarih);
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
        public EczaneNobetSonucTuru SonucTuru { get; set; }
        public string SonucTuruAdi => Enum.GetName(typeof(EczaneNobetSonucTuru), SonucTuru);
        public int NobetGrupGorevTipId { get; set; }
        public DateTime? NobetAltGrupKapanmaTarihi { get; set; }

        string GetEczaneSonuc(int mazeretId, int istekId, string eczaneAdi)
        {
            if (mazeretId > 0)
            {
                return $"{eczaneAdi}: mazeret";
            }
            else if (istekId > 0)
            {
                return $"{eczaneAdi}: istek";
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
                return "4 Kış";
            }
            else if (ay <= 5)
            {
                return "1 İlkbahar";
            }
            else if (ay <= 8)
            {
                return "2 Yaz";
            }
            else if (ay <= 11)
            {
                return "3 Sonbahar";
            }
            else
            {
                return "4 Kış";
            }
        }
        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }


}
