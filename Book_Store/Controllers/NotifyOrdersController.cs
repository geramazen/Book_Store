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
    public class NotifyOrdersController : Controller
    {
        private BookContext db = new BookContext();

        // GET: NotifyOrders
        public ActionResult Index(int? page)
        {
            var recs = db.NotifyOrders.Where(c => c.Status == 0).ToList();
            int pageSize = 12;
            int pageNumber = (page ?? 1);

            return View(recs.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Notified(int ID)
        {
            var Nofify = db.NotifyOrders.Where(c => c.ID == ID).FirstOrDefault();
            Nofify.Status = 1;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: NotifyOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotifyOrder notifyOrder = db.NotifyOrders.Find(id);
            if (notifyOrder == null)
            {
                return HttpNotFound();
            }
            return View(notifyOrder);
        }

        // GET: NotifyOrders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NotifyOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(NotifyOrder notifyOrder)
        {
            if (ModelState.IsValid)
            {
                db.NotifyOrders.Add(notifyOrder);
                db.SaveChanges();
                return RedirectToAction("ViewBooks", "Books", new { Message = "تم إرسال بياناتك، سنخبرك فور وصول الكتاب." });
            }

            return View(notifyOrder);
        }

        // GET: NotifyOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotifyOrder notifyOrder = db.NotifyOrders.Find(id);
            if (notifyOrder == null)
            {
                return HttpNotFound();
            }
            return View(notifyOrder);
        }

        // POST: NotifyOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,BookName,MobileNumber,BID")] NotifyOrder notifyOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notifyOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(notifyOrder);
        }

        // GET: NotifyOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotifyOrder notifyOrder = db.NotifyOrders.Find(id);
            if (notifyOrder == null)
            {
                return HttpNotFound();
            }
            return View(notifyOrder);
        }

        // POST: NotifyOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NotifyOrder notifyOrder = db.NotifyOrders.Find(id);
            db.NotifyOrders.Remove(notifyOrder);
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
    }
}
