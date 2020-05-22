using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WM.EczaneNobet.WebApi.Models
{
    public class EczaneNobetDegisimTalepApi
    {
        public int Id { get; set; }
        public int MyEczaneNobetGrupId { get; set; }
        //public int EczaneNobetDegisimArzId { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int UserId { get; set; }
        public string Aciklama { get; set; }
        public string Token { get; set; }
        public DateTime Tarih { get; set; }

    }
}