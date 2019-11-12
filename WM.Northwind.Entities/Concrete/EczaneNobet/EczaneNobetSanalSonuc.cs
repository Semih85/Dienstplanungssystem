using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.Authorization;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class EczaneNobetSanalSonuc : IEntity
    {
        [Key, ForeignKey("EczaneNobetSonuc")]
        public int EczaneNobetSonucId { get; set; }
        public int UserId { get; set; }
        public DateTime KayitTarihi { get; set; }
        public string Aciklama { get; set; }

        public virtual EczaneNobetSonuc EczaneNobetSonuc { get; set; }
        public virtual User User { get; set; }
    }
}