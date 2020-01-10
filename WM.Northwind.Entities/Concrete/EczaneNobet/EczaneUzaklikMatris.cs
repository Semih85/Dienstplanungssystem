using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class EczaneUzaklikMatris : IEntity
    {
        public int Id { get; set; }
        public int EczaneIdFrom { get; set; }
        public int EczaneIdTo { get; set; }
        public int Mesafe { get; set; }
        public virtual Eczane EczaneFrom { get; set; }
        public virtual Eczane EczaneTo { get; set; }
    }
}