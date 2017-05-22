using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Common
{
    public class MyExcptionAttribute: HandleErrorAttribute
    {
        public static Queue<Exception> ExceptionQueue = new Queue<Exception>();
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            Exception e = filterContext.Exception;
            ExceptionQueue.Enqueue(e);
            filterContext.HttpContext.Response.Redirect("/Error.html");
        }
    }
}