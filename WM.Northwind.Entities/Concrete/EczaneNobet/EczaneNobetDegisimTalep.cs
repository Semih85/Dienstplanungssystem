using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.Authorization;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class EczaneNobetDegisimTalep : IEntity
    {
        public int Id { get; set; }
        public int EczaneNobetDegisimArzId { get; set; }
        //nöbetini değiştirmek isteyen eczanenin nöbetinini tutmak için talip olan eczaneler.
        public int EczaneNobetGrupId { get; set; }
        public int UserId { get; set; }
        //public int EczaneNobetDegisimTalepTipId { get; set; }
        public DateTime KayitTarihi { get; set; }
        [Required(ErrorMessage = "Lütfen açıklama giriniz..")]
        public string Aciklama { get; set; }

        public virtual EczaneNobetGrup EczaneNobetGrup { get; set; }
        public virtual EczaneNobetDegisimArz EczaneNobetDegisimArz { get; set; }
        public virtual User User { get; set; }
    }
}