using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Book_Store.Models;
using System.IO;

namespace Book_Store.Controllers
{
    public class BooksController : Controller
    {
        private BookStoreEntities1 db = new BookStoreEntities1();
        public static List<Book> UserCart;
        public static int count=0;

        // GET: Books
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Author).Include(b => b.Category);
            return View(books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.AID = new SelectList(db.Authors, "AID", "FName");
            ViewBag.CID = new SelectList(db.Categories, "CID", "CName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,title,description,image,AID,CID")] Book book , HttpPostedFileBase imgfile)
        {
            if (ModelState.IsValid)
            {
                string path = "";
                if(imgfile.FileName.Length >0)
                {
                    path = "~/images/" + Path.GetFileName(imgfile.FileName);
                    imgfile.SaveAs(Server.MapPath(path));
                }
                book.image = path;
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AID = new SelectList(db.Authors, "AID", "FName", book.AID);
            ViewBag.CID = new SelectList(db.Categories, "CID", "CName", book.CID);
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AID = new SelectList(db.Authors, "AID", "FName", book.AID);
            ViewBag.CID = new SelectList(db.Categories, "CID", "CName", book.CID);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,title,description,image,AID,CID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AID = new SelectList(db.Authors, "AID", "FName", book.AID);
            ViewBag.CID = new SelectList(db.Categories, "CID", "CName", book.CID);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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

        public ActionResult ViewBooks()
        {
            var recs = db.Books.ToList();
            //if (Session["UserName"] != null)
            //{
                return View(recs);
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Users");
            //}
        }

        public ActionResult AddToCart(int id)
        {
            count++;
            Session["Cart"] = count;
            Book book = db.Books.Find(id);
            UserCart = new List<Book>()
            {
                new Book() {ID= book.ID , title= book.title , AID=book.AID , description=book.description }
            };
            return RedirectToAction("ViewBooks");
        }

        public ActionResult ViewCart()
        {
            var books = UserCart;
            return View(books.ToList());
        }
    }
}
