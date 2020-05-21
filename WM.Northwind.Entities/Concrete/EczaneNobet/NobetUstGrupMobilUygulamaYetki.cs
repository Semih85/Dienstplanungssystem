using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class NobetUstGrupMobilUygulamaYetki : IEntity
    {
        public int Id { get; set; }
        public int NobetUstGrupId { get; set; }
        public int MobilUygulamaYetkiId { get; set; }

        public virtual MobilUygulamaYetki MobilUygulamaYetki { get; set; }
        public virtual NobetUstGrup NobetUstGrup { get; set; }
    }
}