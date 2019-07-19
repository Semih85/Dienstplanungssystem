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
    public class AlternatifOlasiNobetciEczaneler : IComplexType
    {
        public string EczaneAdi { get; set; }
        public string Mazereti { get; set; }
        public DateTime? OncekiNobetTarihi { get; set; }
        public DateTime? SonrakiNobetTarihi { get; set; }
        public string OncekiNobet => OncekiNobetTarihi != DateTime.Today ? $"{((DateTime)OncekiNobetTarihi).ToString("dd MMM yyyy, ddd.")} ({(Tarih - (DateTime)OncekiNobetTarihi).TotalDays} gün)" : "-";
        public string SonrakiNobet => SonrakiNobetTarihi != DateTime.Today ? $"{((DateTime)SonrakiNobetTarihi).ToString("dd MMM yyyy, ddd.")} ({((DateTime)SonrakiNobetTarihi - Tarih).TotalDays} gün)" : "-";
        public int Mesafe { get; set; }
        public DateTime Tarih { get; set; }
        public int NobetSayisiToplam { get; set; }
        public int NobetSayisiGunGrup { get; set; }
    }
}