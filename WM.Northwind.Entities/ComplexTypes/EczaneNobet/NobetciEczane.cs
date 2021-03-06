﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class NobetciEczane : IComplexType
    {
        public int EczaneId { get; set; }
        public int NobetUstGrupId { get; set; }
        public string Adi { get; set; }
        public string Adres { get; set; }
        public string TelefonNo { get; set; }
        public double Enlem { get; set; }
        public double Boylam { get; set; }
        public string AdresTarifi { get; set; }
        public string AdresTarifiKisa { get; set; }
        public string NobetGorevTipAdi { get; set; }
        public string NobetGrupAdi { get; set; }
        public string NobetAltGrupAdi { get; set; }
        public string EczaneninAcikOlduguSaatAraligi { get; set; }
        public DateTime KapanisSaati => KapanisSaatiniGetir(EczaneninAcikOlduguSaatAraligi);

        //public DateTime KapanisSaati =>
        //    NobetGorevTipAdi != null ?
        //    DateTime.Now.Hour >= 9 ?
        //    DateTime.Today.AddDays(1).AddHours(Convert.ToDouble(NobetGorevTipAdi.Substring(8, 2))).AddMinutes(Convert.ToDouble(NobetGorevTipAdi.Substring(11, 2)))
        //    : DateTime.Today.AddHours(Convert.ToDouble(NobetGorevTipAdi.Substring(8, 2))).AddMinutes(Convert.ToDouble(NobetGorevTipAdi.Substring(11, 2)))
        //    : DateTime.Now;

        private DateTime KapanisSaatiniGetir(string eczaneninAcikOlduguSaatAraligi) {

            var kapanisSaati = DateTime.Now;

            var t1 = Convert.ToDouble(eczaneninAcikOlduguSaatAraligi.Substring(8, 2));
            var t2 = Convert.ToDouble(eczaneninAcikOlduguSaatAraligi.Substring(11, 2));

            kapanisSaati = kapanisSaati != null ?
                DateTime.Now.Hour >= 9 ?
                DateTime.Today.AddDays(1).AddHours(t1).AddMinutes(t2)
                : DateTime.Today.AddHours(t1).AddMinutes(t2)
                : DateTime.Now;

            return kapanisSaati;
        }
    }
}
