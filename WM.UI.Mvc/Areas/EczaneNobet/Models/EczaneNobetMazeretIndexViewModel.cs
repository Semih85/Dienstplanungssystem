using System;
using System.Linq;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class EczaneNobetMazeretIndexViewModel
    {        
        public DateTime? BaslangicTarihi { get; internal set; }
        public DateTime? BitisTarihi { get; internal set; }
        public int Id { get; internal set; }
    }
}