using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class EczaneIlce : IEntity
    {
        public int Id { get; set; }
        public int EczaneId { get; set; }
        public int IlceId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        //Bitiş tarihi null ise, bu eczane o grupta aktiftir.
        public DateTime? BitisTarihi { get; set; }
        //Aciklamada nöbet grubu değişikliğinin gerekçesi belirtilecektir.
        public string Aciklama { get; set; }

        public virtual Eczane Eczane { get; set; }
        public virtual Ilce Ilce { get; set; }
    }
}
