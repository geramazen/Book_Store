using System.Web.Mvc;
using System.Web.SessionState;

namespace Book_Store.Helpers
{
    public class PVController : Controller
    {
        // GET: PV   
        public ActionResult ReturnView()
        {
            return RedirectToAction("ViewBooks", "Books", new { Message = "عذرا, ليس لديك صلاحيةالوصول  " });
            
        }

    }
}