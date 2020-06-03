using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class EczaneMobilBildirim : IEntity
    {
        public int Id { get; set; }
        public int EczaneId { get; set; }
        public int MobilBildirimId { get; set; }
        public DateTime? BildirimGormeTarihi { get; set; }
        public virtual Eczane Eczane { get; set; }
        public virtual MobilBildirim MobilBildirim { get; set; }

    }
}