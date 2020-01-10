using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class EczaneNobetMazeret: IEntity
    {
        public int Id { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int MazeretId { get; set; }
        public int TakvimId { get; set; }
        public string Aciklama { get; set; }

        public virtual EczaneNobetGrup EczaneNobetGrup { get; set; }
        public virtual Mazeret Mazeret { get; set; }
        public virtual Takvim Takvim { get; set; }
    }
}
