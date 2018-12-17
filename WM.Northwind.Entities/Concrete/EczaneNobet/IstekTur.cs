using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class IstekTur: IEntity
    {
        public int Id { get; set; }
        public string Adi { get; set; }

        public virtual List<Istek> Istekler { get; set; }
    }
}
