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
    public class EczaneNobetGrupAltGrup : IEntity
    {
        //[Key, ForeignKey("EczaneNobetGrup")]
        public int Id { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int NobetAltGrupId { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Başlangıç Tarihi")]
        public DateTime BaslangicTarihi { get; set; }
        //Bitiş tarihi null ise, bu eczane o grupta aktiftir.
        [Display(Name = "Bitiş Tarihi")]
        public DateTime? BitisTarihi { get; set; }
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }

        public virtual EczaneNobetGrup EczaneNobetGrup { get; set; }
        public virtual NobetAltGrup NobetAltGrup { get; set; }
    }
}