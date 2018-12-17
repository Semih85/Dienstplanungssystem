using System;
using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class NobetciEczaneHaritaViewModel
    {
        public NobetciEczaneHaritaViewModel()
        {
        }
        
        public double Enlem { get; set; }
        public double Boylam { get; set; }
        public DateTime Tarih { get; set; }
        public List<NobetciEczane> NobetciEczaneler { get; set; }
    }
}