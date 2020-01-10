using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class EczaneGorevTip: IEntity
    {
        public int Id { get; set; }
        public int EczaneId { get; set; }
        public int GorevTipId { get; set; }

        public virtual Eczane Eczane { get; set; }
        public virtual GorevTip GorevTip { get; set; }

    } 
} 