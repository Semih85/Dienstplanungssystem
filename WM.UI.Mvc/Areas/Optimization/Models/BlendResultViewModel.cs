using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Optimization.Blend;

namespace WM.UI.Mvc.Areas.Optimization.Models
{
    public class BlendResultViewModel
    {
        public BlendResultModel blendResultModel { get; set; }
    }
}