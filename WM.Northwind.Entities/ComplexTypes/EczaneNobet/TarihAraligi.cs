using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class TarihAraligi
    {
        public int TakvimId { get; set; }
        public int Yil { get; set; }
        public int Ay { get; set; }
        public int GunId { get; set; }
        //bayram olduğunda haftanın gününü değiştirir
        public int GunDegerId { get; set; }
        //orjinal haftanın günleri
        public int HaftaninGunu { get; set; }
    }
}
