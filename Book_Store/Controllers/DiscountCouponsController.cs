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
    public class DiscountCouponsController : Controller
    {
        private BookContext db = new BookContext();

        // GET: DiscountCoupons
        [Helpers.AdminAccess]
        public ActionResult Index(int? page)
        {
            var recs = db.DiscountCoupons.ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(recs.ToPagedList(pageNumber, pageSize));

        }

        // GET: DiscountCoupons/Details/5
        [Helpers.AdminAccess]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountCoupon discountCoupon = db.DiscountCoupons.Find(id);
            if (discountCoupon == null)
            {
                return HttpNotFound();
            }
            return View(discountCoupon);
        }

        // GET: DiscountCoupons/Create
        [Helpers.AdminAccess]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DiscountCoupons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Helpers.AdminAccess]
        public ActionResult Create([Bind(Include = "ID,Name,percentage")] DiscountCoupon discountCoupon)
        {
            if (ModelState.IsValid)
            {
                db.DiscountCoupons.Add(discountCoupon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(discountCoupon);
        }

        // GET: DiscountCoupons/Edit/5
        [Helpers.AdminAccess]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountCoupon discountCoupon = db.DiscountCoupons.Find(id);
            if (discountCoupon == null)
            {
                return HttpNotFound();
            }
            return View(discountCoupon);
        }

        // POST: DiscountCoupons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Helpers.AdminAccess]
        public ActionResult Edit([Bind(Include = "ID,Name,percentage")] DiscountCoupon discountCoupon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discountCoupon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(discountCoupon);
        }

        // GET: DiscountCoupons/Delete/5
        [Helpers.AdminAccess]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountCoupon discountCoupon = db.DiscountCoupons.Find(id);
            if (discountCoupon == null)
            {
                return HttpNotFound();
            }
            return View(discountCoupon);
        }

        // POST: DiscountCoupons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Helpers.AdminAccess]
        public ActionResult DeleteConfirmed(int id)
        {
            DiscountCoupon discountCoupon = db.DiscountCoupons.Find(id);
            db.DiscountCoupons.Remove(discountCoupon);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public JsonResult CheckCoupon(string DiscountCoupon)
        {
            var Coupon = db.DiscountCoupons.Where(c => c.Name == DiscountCoupon).FirstOrDefault();
            if(Coupon != null)
            {
                var perc = Convert.ToInt32(Coupon.percentage);
                return Json(new { status = 1, Message = "سيتم خصم" + perc + "% من قيمة الطلب" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = 0}, JsonRequestBehavior.AllowGet);
            }
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
