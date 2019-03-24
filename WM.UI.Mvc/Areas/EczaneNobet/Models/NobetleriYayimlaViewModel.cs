using System;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class NobetleriYayimlaViewModel
    {
        public int Yil { get; set; }
        public int Ay { get; set; }
        public int NobetGrupId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public string NobetGrupAdi { get; internal set; }
        public bool YayimlandiMi { get; internal set; }
    }
}