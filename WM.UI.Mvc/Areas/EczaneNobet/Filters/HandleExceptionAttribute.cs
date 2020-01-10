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
            if (filterContext.Exception.Message.StartsWith("Aşağıdaki"))
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Areas/EczaneNobet/Views/Shared/ErrorModelCoz.cshtml"
                };
            }
            else
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error"
                };
            }
        }
    }
}