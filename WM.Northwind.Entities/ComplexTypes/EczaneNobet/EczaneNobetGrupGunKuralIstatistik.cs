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
    public class EczaneNobetGrupGunKuralIstatistik : IComplexType
    {
        public int EczaneNobetGrupId { get; set; }
        public DateTime EczaneNobetGrupBaslamaTarihi { get; set; }
        
        public int EczaneId { get; set; }
        public string EczaneAdi { get; set; }
        public string NobetGrupAdi { get; set; }
        public string GunGrup { get; set; }
        public int NobetGrupId { get; set; }
        public int NobetUstGrupId { get; set; }
        public int NobetGorevTipId { get; set; }
        public int NobetAltGrupId { get; set; }
        public int NobetGunKuralId { get; set; }
        public int NobetSayisi { get; set; }
        public int NobetSayisiGercek { get; set; }
        public DateTime IlkNobetTarihi { get; set; }
        public DateTime SonNobetTarihi { get; set; }
    }
}
