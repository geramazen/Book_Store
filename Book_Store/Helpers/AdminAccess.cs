using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Net;
using System.Web.Mvc;

namespace Book_Store.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminAccess : ActionFilterAttribute
    {
        private static PVController pv = new PVController();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Session["UserName"] = rec.Name;
            //Session["UserID"] = rec.ID;
            var user = filterContext.HttpContext.Session["UserName"];
            if (user!=null&&user.ToString()== "Admin")
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            else
            {
                filterContext.Result = pv.ReturnView();
            }
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }


    }
}