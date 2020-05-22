using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WM.EczaneNobet.WebApi.Models
{
    public class EczaneApi
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string AdresTarifi { get; set; }
        public string AdresTarifiKisa { get; set; }
        public DateTime AcilisTarihi { get; set; }
        public DateTime? KapanisTarihi { get; set; }
        public double Enlem { get; set; }
        public double Boylam { get; set; }


    }
}