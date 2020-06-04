using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class MobilBildirim : IEntity
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Metin { get; set; }
        public string Aciklama { get; set; }
        public int NobetUstGrupId { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yy H:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime GonderimTarihi { get; set; }
        public virtual NobetUstGrup NobetUstGrup { get; set; }

        public virtual List<EczaneMobilBildirim> EczaneMobilBildirimler { get; set; }
    }
}