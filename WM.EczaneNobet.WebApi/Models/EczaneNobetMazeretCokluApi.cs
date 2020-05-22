using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WM.EczaneNobet.WebApi.Models
{
    public class EczaneNobetMazeretCokluApi
    {
        public int Id { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int MazeretId { get; set; }
        public string Aciklama { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public DateTime[] Tarihler { get; set; }
    }
}