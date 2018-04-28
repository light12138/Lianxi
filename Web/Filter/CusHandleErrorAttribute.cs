using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Filter
{
    public class CusHandleErrorAttribute: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            //设置为true阻止golbal里面的错误执行
            filterContext.ExceptionHandled = true;
            //filterContext.HttpContext.Response.Write(filterContext.Exception.Message);
            filterContext.Result = new JsonResult { Data=new { id="1", name="wss" },JsonRequestBehavior=JsonRequestBehavior.AllowGet };
        }
    }
}