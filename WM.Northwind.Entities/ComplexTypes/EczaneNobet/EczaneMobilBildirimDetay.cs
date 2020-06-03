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
    public class EczaneMobilBildirimDetay: IComplexType
 { 
        public int Id { get; set; }
        public int EczaneId { get; set; }
        public int MobilBildirimId { get; set; }
        public DateTime? BildirimGormeTarihi { get; set; }
        public string EczaneAdi { get; set; }
        public string MobilBildirimBaslik { get; set; }
        public string MobilBildirimMetin { get; set; }

    } 
} 