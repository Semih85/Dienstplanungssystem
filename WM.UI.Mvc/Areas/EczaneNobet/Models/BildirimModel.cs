using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WM.Northwind.Entities.Concrete.Authorization;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class BildirimModel
    {
        public string Baslik { get; set; }
        public string Metin { get; set; }
        [Display(Name = "Kullanıcı")]
        public int[] UserId { get; set; }
    }
}