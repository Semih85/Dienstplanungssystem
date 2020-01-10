using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class NobetGrupGunKural: IEntity
    {
        public int Id { get; set; }
        public int NobetGrupId { get; set; }
        public int NobetGunKuralId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }

        public virtual NobetGrup NobetGrup { get; set; }
        public virtual NobetGunKural NobetGunKural { get; set; }
    } 
} 