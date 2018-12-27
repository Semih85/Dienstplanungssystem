using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WM.UI.Mvc.Areas.EczaneNobet.Filters
{
    public class HandleExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            throw new NotImplementedException();
        }
    }
}