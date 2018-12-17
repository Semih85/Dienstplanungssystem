using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class EczaneNobetIstek : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Eczane")]
        public int EczaneNobetGrupId { get; set; }
        public int IstekId { get; set; }
        [Display(Name = "Tarih")]
        public int TakvimId { get; set; }
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }

        public virtual EczaneNobetGrup EczaneNobetGrup { get; set; }
        public virtual Istek Istek { get; set; }
        public virtual Takvim Takvim { get; set; }

    }
}
