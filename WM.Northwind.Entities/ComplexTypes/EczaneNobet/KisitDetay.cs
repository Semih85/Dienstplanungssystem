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
    public class KisitDetay : IComplexType
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string AdiGosterilen { get; set; }
        public string Aciklama { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public int KisitKategoriId { get; set; }
        public string KisitKategoriAdi { get; set; }
        public string KisitAdi => $"K{(Id < 10 ? '0' + Id.ToString() : Id.ToString())} {KisitKategoriAdi}, {AdiGosterilen}";
        public bool DegerPasifMi { get; set; }
    }
}
