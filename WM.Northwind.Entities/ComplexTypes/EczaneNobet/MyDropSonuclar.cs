using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class MyDropSonuclar
    {
        //public MyDrop()
        //{
        //    IdValue = $"{Id} {Value}";
        //}

        public int Id { get; set; }
        public string Value { get; set; }

        public DateTime Tarih { get; set; }
    }
}
