using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class EczaneNobetFeragat : IEntity
    {
        [Key, ForeignKey("EczaneNobetSonuc")]
        public int EczaneNobetSonucId { get; set; }
        [Required(ErrorMessage = "Feragat Açıklaması gereklidir.")]
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
        public int NobetFeragatTipId { get; set; }
        public int EczaneNobetGrupId { get; set; }

        public virtual EczaneNobetSonuc EczaneNobetSonuc { get; set; }
        public virtual NobetFeragatTip NobetFeragatTip { get; set; }
        public virtual EczaneNobetGrup EczaneNobetGrup { get; set; }
    }
}