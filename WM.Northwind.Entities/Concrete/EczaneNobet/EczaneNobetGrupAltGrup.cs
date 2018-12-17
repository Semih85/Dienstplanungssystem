using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class EczaneNobetGrupAltGrup : IEntity
    {
        [Key, ForeignKey("EczaneNobetGrup")]
        public int EczaneNobetGrupId { get; set; }
        public int NobetAltGrupId { get; set; }

        public virtual EczaneNobetGrup EczaneNobetGrup { get; set; }
        public virtual NobetAltGrup NobetAltGrup { get; set; }
    }
}