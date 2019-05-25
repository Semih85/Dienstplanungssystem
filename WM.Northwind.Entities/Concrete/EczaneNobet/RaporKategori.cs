﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class RaporKategori : IEntity
    {
        public int Id { get; set; }
        public string Adi { get; set; }

        public List<Rapor>  Raporlar { get; set; }
    }
}