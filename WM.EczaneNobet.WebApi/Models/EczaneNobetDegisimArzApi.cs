using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WM.EczaneNobet.WebApi.Models
{
    public class EczaneNobetDegisimArzApi
    {
        public int Id { get; set; }
        public int EczaneNobetSonucId { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int UserId { get; set; }
        public string Aciklama { get; set; }
        public string Token { get; set; }
        public DateTime Tarih { get; set; }

    }
}