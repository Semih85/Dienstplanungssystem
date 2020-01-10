using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class Kalibrasyon : IEntity
    {
        public int Id { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int KalibrasyonTipId { get; set; }
        public int NobetUstGrupGunGrupId { get; set; }
        public double Deger { get; set; }
        public string Aciklama { get; set; }
        
        public virtual NobetUstGrupGunGrup NobetUstGrupGunGrup { get; set; }
        public virtual EczaneNobetGrup EczaneNobetGrup { get; set; }
        public virtual KalibrasyonTip KalibrasyonTip { get; set; }
    }
}