using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Book_Store.Models;
using PagedList;

namespace Book_Store.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly BookContext db = new BookContext();

        // GET: Authors
        public ActionResult Index(int? page)
        {
            var recs = db.Authors.ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(recs.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult UserIndex(int? page, string Author)
        {
            var recs = db.Authors.ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if(Author != null)
            {
                recs = recs.Where(c => c.FName.Contains(Author)).ToList();
            }

            return View(recs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Authors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AID,FName,LName")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }

        // GET: Authors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AID,FName,LName")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            Author author = db.Authors.Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Authors/Delete/5
  

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Authors/Edit/5
        public ActionResult AllWork(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var books = db.Books.Where(b => b.AID == id).ToList();
            if (books == null)
            {
                return HttpNotFound();
            }
            return View("../Books/ViewBooks", books);
        }


        public ActionResult AuthorBooks(int? page ,int AID)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var Books = db.Books.Where(b => b.AID == AID).ToList();
            return View(Books.ToPagedList(pageNumber, pageSize));
        }
    }
}
