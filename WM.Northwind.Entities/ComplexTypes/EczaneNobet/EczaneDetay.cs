﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneDetay : IComplexType
    {
        public int Id { get; set; }
        [Display(Name = "Eczane")]
        public string EczaneAdi { get; set; }   

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Açılış Tarihi")]
        [Required(ErrorMessage = "Açılış Tarihi gereklidir.")]
        public DateTime AcilisTarihi { get; set; }

        [Display(Name = "Kapanış Tarihi")]
        public DateTime? KapanisTarihi { get; set; }
        public int NobetUstGrupId { get; set; }
        [Display(Name = "Nöbet Ust Grubu")]
        public string NobetUstGrupAdi { get; set; }

        //koordinat
        [Required(ErrorMessage = "Enlem gereklidir. Bilmiyorsanız 0 giriniz.")]
        public double Enlem { get; set; }
        [Required(ErrorMessage = "Boylam gereklidir. Bilmiyorsanız 0 giriniz.")]
        public double Boylam { get; set; }
        
        [Display(Name = "Web Sitesi")]
        public string WebSitesi { get; set; }
        [Display(Name = "Mail Adresi")]
        public string MailAdresi { get; set; }
        [Display(Name = "Telefon Numarası")]
        public string TelefonNo { get; set; }
        public string Adres { get; set; }
        [Display(Name = "Adres Tarifi")]
        public string AdresTarifi { get; set; }
        [Display(Name = "Adres Tarifi Kısa")]
        public string AdresTarifiKisa { get; set; }
    }
}
