using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class NobetGrupGorevTipTakvimOzelGun : IEntity
    {
        public int Id { get; set; }
        public int TakvimId { get; set; }
        public int NobetGunKuralId { get; set; }
        public int NobetGrupGorevTipGunKuralId { get; set; }
        public int NobetOzelGunId { get; set; }//bayramTurId -- eski adı
        public bool FarkliGunGosterilsinMi { get; set; }//true ise NobetGunKuralId, değilse -normalde- NobetGrupGorevTipGunKuralId
        public double AgirlikDegeri { get; set; }
        public int NobetOzelGunKategoriId { get; set; }

        public virtual NobetGrupGorevTipGunKural NobetGrupGorevTipGunKural { get; set; }
        public virtual NobetGunKural NobetGunKural { get; set; }
        public virtual NobetOzelGun NobetOzelGun { get; set; }
        public virtual Takvim Takvim { get; set; }
        public virtual NobetOzelGunKategori NobetOzelGunKategori { get; set; }
    }
}