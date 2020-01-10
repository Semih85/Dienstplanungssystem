using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class RaporNobetUstGrup : IEntity
    {
        public int Id { get; set; }
        public int RaporId { get; set; }
        public int NobetUstGrupId { get; set; }
        public virtual NobetUstGrup NobetUstGrup { get; set; }
        public virtual Rapor Rapor { get; set; }
    }
}