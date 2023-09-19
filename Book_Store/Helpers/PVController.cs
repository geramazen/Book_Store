using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book_Store.Helpers
{
    public class PVController : Controller
    {
        // GET: PV   
        public ActionResult ReturnView()
        {
            return RedirectToAction("ViewBooks", "Books", new { Message = "عذرا, ليس لديك صلاحيةالوصول  " });
            
        }

        public bool IsAdmin()
        {
            if(Session !=null && Session["UserName"] != null && Session["UserName"].ToString() == "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}