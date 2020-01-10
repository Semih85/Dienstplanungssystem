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
    public class RaporRolDetay: IComplexType
 { 
        public int Id { get; set; }
        public int RaporId { get; set; }
        public int RoleId { get; set; }
        public string RaporAdi { get; set; }
        public string RoleAdi { get; set; }
        public string RaporKategoriAdi { get; set; }
        public int RaporKategoriId { get; set; }
    } 
} 