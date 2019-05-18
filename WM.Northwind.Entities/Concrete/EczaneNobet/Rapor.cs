using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class Rapor : IEntity
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public int RaporKategoriId { get; set; }
        public int SiraId { get; set; }
        public virtual RaporKategori RaporKategori { get; set; }

        public virtual List<RaporRol>  RaporRoller { get; set; }
        public virtual List<RaporNobetUstGrup>  RaporNobetUstGruplar { get; set; }
    }
}