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
    public class PublishersController : Controller
    {
        private readonly BookContext db = new BookContext();

        // GET: Publishers
        [Helpers.AdminAccess]
        public ActionResult Index(int? page,string Publisher)
        {
            var recs = db.publishers.ToList();
            if (Publisher != null)
            {
                recs = recs.Where(c => c.PName.Contains(Publisher)).ToList();
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(recs.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult UserIndex(int? page, string PublisherName)
        {
            var recs = db.publishers.ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (PublisherName != null)
            {
                recs = recs.Where(c => c.PName.Contains(PublisherName)).ToList();
            }

            return View(recs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Publishers/Details/5
        [Helpers.AdminAccess]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = db.publishers.Find(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        // GET: Publishers/Create
        [Helpers.AdminAccess]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Helpers.AdminAccess]
        public ActionResult Create([Bind(Include = "PID,PName")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                db.publishers.Add(publisher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(publisher);
        }

        // GET: Publishers/Edit/5
        [Helpers.AdminAccess]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = db.publishers.Find(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Helpers.AdminAccess]
        public ActionResult Edit([Bind(Include = "PID,PName")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publisher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        [Helpers.AdminAccess]
        public ActionResult Delete(int? id)
        {
            Publisher publisher = db.publishers.Find(id);
            db.publishers.Remove(publisher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Publishers/Delete/5
  

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //public ActionResult PublisherBooks(int? page, int PID)
        //{
        //    int pageSize = 10;
        //    int pageNumber = (page ?? 1);
        //    var Books = db.Books.Where(b => b.PID == PID).ToList();
        //    return View(Books.ToPagedList(pageNumber, pageSize));
        //}
    }
}
