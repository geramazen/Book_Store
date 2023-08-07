using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Book_Store.Models;

namespace Book_Store.Controllers
{
    public class ShippingCostsController : Controller
    {
        private BookContext db = new BookContext();

        // GET: ShippingCosts
        public ActionResult Index()
        {
            return View(db.shippingCosts.ToList());
        }

        // GET: ShippingCosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingCost shippingCost = db.shippingCosts.Find(id);
            if (shippingCost == null)
            {
                return HttpNotFound();
            }
            return View(shippingCost);
        }

        // GET: ShippingCosts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShippingCosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Cost")] ShippingCost shippingCost)
        {
            if (ModelState.IsValid)
            {
                db.shippingCosts.Add(shippingCost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shippingCost);
        }

        // GET: ShippingCosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingCost shippingCost = db.shippingCosts.Find(id);
            if (shippingCost == null)
            {
                return HttpNotFound();
            }
            return View(shippingCost);
        }

        // POST: ShippingCosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Cost")] ShippingCost shippingCost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shippingCost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shippingCost);
        }

        // GET: ShippingCosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingCost shippingCost = db.shippingCosts.Find(id);
            if (shippingCost == null)
            {
                return HttpNotFound();
            }
            return View(shippingCost);
        }

        // POST: ShippingCosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShippingCost shippingCost = db.shippingCosts.Find(id);
            db.shippingCosts.Remove(shippingCost);
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
