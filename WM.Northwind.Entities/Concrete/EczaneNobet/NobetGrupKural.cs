using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class NobetGrupKural: IEntity
    {
        public int Id { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public int NobetKuralId { get; set; }
        [Display(Name = "Başlangıç Tarihi")]
        public DateTime BaslangicTarihi { get; set; }
        [Display(Name = "Bitiş Tarihi")]
        public DateTime? BitisTarihi { get; set; }
        [Display(Name = "Değer")]
        public double? Deger { get; set; }

        public virtual NobetGrupGorevTip NobetGrupGorevTip { get; set; }
        public virtual NobetKural NobetKural { get; set; }


    } 
} 