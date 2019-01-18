using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetTarihAralik: IComplexType
    {
        public DateTime Tarih { get; set; }
        public int TakvimId { get; set; }
        //public int Yil { get; set; }
        public int Ay => Tarih.Month;
        //public int Gun { get; set; }
        public string AyIkili => GetIkiliAylar(Ay);
        public int NobetGunKuralId { get; set; }
        public string NobetGunKuralAdi { get; set; }
        public int GunGrupId { get; set; }
        public string GunGrupAdi { get; set; }
        public bool CumartesiGunuMu { get; set; }
        public bool PazarGunuMu { get; set; }
        public bool BayramMi { get; set; }
        public bool HaftaIciMi { get; set; }
        public bool ArifeMi { get; set; }

        public int NobetGrupGorevTipId { get; set; }
        
        public int EczaneId { get; set; }
        public int NobetGrupId { get; set; }
        public string EczaneAdi { get; set; }
        public string NobetGrupAdi { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int NobetGorevTipId { get; set; }
        public string NobetGorevTipAdi { get; set; }
        public int HaftaninGunu { get; set; }

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
    }
}
