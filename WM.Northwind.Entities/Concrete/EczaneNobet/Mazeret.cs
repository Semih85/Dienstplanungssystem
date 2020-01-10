using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class Mazeret: IEntity
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public int MazeretTurId { get; set; }

        public virtual MazeretTur MazeretTur { get; set; }
        public virtual List<EczaneNobetMazeret> EczaneNobetMazeretler { get; set; }
    }
}
