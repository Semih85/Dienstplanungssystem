using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class EczaneNobetSonuc: IEntity
    {
        public int Id { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int NobetGorevTipId { get; set; }
        public int TakvimId { get; set; }
        public bool YayimlandiMi { get; set; }

        public virtual NobetGorevTip NobetGorevTip { get; set; }
        public virtual EczaneNobetGrup EczaneNobetGrup { get; set; }
        public virtual EczaneNobetFeragat EczaneNobetFeragat { get; set; }
        public virtual Takvim Takvim { get; set; }
        public virtual List<EczaneNobetDegisim> EczaneNobetDegisimler { get; set; }
        public virtual EczaneNobetSanalSonuc EczaneNobetSanalSonuc { get; set; }
    }
}
