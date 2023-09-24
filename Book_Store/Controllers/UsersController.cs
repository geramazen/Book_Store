using Book_Store.Helpers;
using Book_Store.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Book_Store.Controllers
{
    public class UsersController : Controller
    {
        private BookContext db = new BookContext();

        // GET: Users
        [AdminAccess]
        public ActionResult Index()
        {
            if (Session["UserType"] != null)
            {
                return View(db.Users.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

        // GET: Users/Details/5
        [AdminAccess]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View(new User());
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,EMAIL,Phone,Address,UserName,Password")] User user)
        {
            if (db.Users.Any(x => x.EMAIL == user.EMAIL))
            {
                ModelState.AddModelError("Email", "This Email Is Already Registered!, Login Instead.");
            }

            if (db.Users.Any(x => x.UserName == user.UserName))
            {
                ModelState.AddModelError("UserName", "This UserName Is Already used!, Try Another UserName.");
            }

            if (db.Users.Any(x => x.Phone == user.Phone))
            {
                ModelState.AddModelError("Phone", "This Phone Is Already Registered!, Login Instead.");
            }

            if (user.UserName.Length < 6 || user.Password.Length < 6)
            {
                ModelState.AddModelError("Length", "UserName and Password Must Contain More Than or Equal to 6 Characters");
            }

            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        [AdminAccess]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminAccess]
        public ActionResult Edit([Bind(Include = "ID,Name,EMAIL,Password,role,image")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        [AdminAccess]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AdminAccess]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        [HttpGet]
        [ActionName("Registar")]
        public ActionResult Registar_Get()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Registar")]
        public ActionResult Registar_Post([Bind(Include = "Name,EMAIL,ConfEmail,Password,ConfPassword")] User user)
        {          
            if (db.Users.Any(x => x.EMAIL == user.EMAIL))
            {
                ModelState.AddModelError("Email","This Email Is Already Registered!, Login Instead.");
            }

            if (db.Users.Any(x => x.UserName == user.UserName))
            {
                ModelState.AddModelError("UserName", "This UserName Is Already used!, Try Another UserName.");
            }

            if (db.Users.Any(x => x.Phone == user.Phone))
            {
                ModelState.AddModelError("Phone", "This Phone Is Already Registered!, Login Instead.");
            }

            if (user.UserName.Length<6 || user.Password.Length < 6)
            {
                ModelState.AddModelError("Length", "UserName and Password Must Contain More Than or Equal to 6 Characters");
            }

            if (ModelState.IsValid)
            {              
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            return View(user);
        }

        [HttpGet, ActionName("Login")]
        public ActionResult Login_get()
        {
            return View();
        }

        [HttpPost, ActionName("Login")]
        public ActionResult Login_post(User user)
        {
            var rec = db.Users.Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefault();
            if(rec != null)
            {
                Session["UserName"] = rec.UserName;
                Session["UserID"] = rec.ID;
                Session["Cart"] = 0;
                
                return RedirectToAction("ViewBooks", "Books");
            }
            else
            {
                ViewBag.error = "invalid user";
                return View(user);

            }
        }

        public ActionResult Logout()
        {
            Session["UserName"] = null;
            Session["UserID"] = null;
            Session["Cart"] = null;

            return RedirectToAction("Login");
        }
   
        //public ActionResult MakeAdmin(int? id)
        //{
        //    User user = db.Users.Find(id);
        //    var Admin = "Admin";
        //    user.role = Admin;
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


    }
}
