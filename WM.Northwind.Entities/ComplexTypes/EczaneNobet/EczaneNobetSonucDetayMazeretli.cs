using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetSonucDetayMazeretli : EczaneNobetSonucDetay2, IComplexType
    {
        public string MazeretAdi { get; set; }
        public int MesafeMetre { get; set; }
    }
}

