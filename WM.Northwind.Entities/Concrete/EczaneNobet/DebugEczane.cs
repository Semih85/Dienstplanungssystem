using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class DebugEczane : IEntity
    {
        public int Id { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public bool AktifMi { get; set; }
        public virtual EczaneNobetGrup EczaneNobetGrup { get; set; }
    }
}
