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
    public class OrdersController : Controller
    {
        private BookContext db = new BookContext();

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            var Orders = db.Orders.Where(o => o.OrderID == id).ToList();
            return View(Orders);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<Book> Books, Order order)
        {
            var OrderId = 1;
            if (db.Orders.Count() != 0)
            {
                OrderId = db.Orders.Select(c => c.OrderID).Max() + 1;
            }
            foreach (var item in Books)
            {
                Order DBorder = new Order
                {
                    Adress = order.Adress,
                    Amount = order.Amount,
                    BookName = item.title,
                    OrderID = OrderId,
                    MobileNumber = order.MobileNumber,
                    Name = order.Name,
                    Order_Status = 0,
                    PublisherName = item.Publisher.PName
                };
                db.Orders.Add(DBorder);
            }
            db.SaveChanges();
            ViewBag.OrderID = 0;
            return RedirectToAction("ViewBooks", "Books", new { Message = "تم إرسال طلبك" });
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Adress,MobileNumber,Order_Status,OrderID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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


        public ActionResult OrdersToConfirm()
        {
            var Orders = db.Orders.Where(c => c.Order_Status == 0).ToList();
            return View(Orders);
        }

        public ActionResult OrdersToShip()
        {
            var Orders = db.Orders.Where(c => c.Order_Status == 1).ToList();
            return View(Orders);
        }

        public ActionResult OrderShiped(int OrderID)
        {
            var AllOrders = db.Orders.Where(o => o.OrderID == OrderID).ToList();
            foreach (var item in AllOrders)
            {
                item.Order_Status = 2;
            }
            db.SaveChanges();
            return RedirectToAction("ViewBooks", "Books");
        }

        public ActionResult ConfirmOrder(int OrderID)
        {
            var AllOrders = db.Orders.Where(o => o.OrderID == OrderID).ToList();
            foreach (var item in AllOrders)
            {
                item.Order_Status = 1;
            }
            db.SaveChanges();
            return RedirectToAction("ViewBooks", "Books");
        }

        public ActionResult CheckOrder(int OrderID)
        {
            var OrderStatusMessage = "";
            var Order = db.Orders.Where(c => c.OrderID == OrderID).FirstOrDefault();
            if (Order == null)
            {
                OrderStatusMessage = ".كود الطلب غير صحيح, تحقق من الكود ثم أعد المحاولة";
                return RedirectToAction("OrderFollowing_Result", new { OrderStatusMessage = OrderStatusMessage, OrderID = 0 });
            }
            else
            {
                var OrderStatus = Order.Order_Status;
                if (OrderStatus == 0)
                {
                    OrderStatusMessage = ".تم إرسال طلبك إلينا وسيتم تأكيده قريبا";
                }
                else if (OrderStatus == 1)
                {
                    OrderStatusMessage = ".تم وصول طلبك إلينا وتأكيده، وسيتم شحنه عبر شركة الشحن قريبا";
                }
                else if (OrderStatus == 2)
                {
                    OrderStatusMessage = ".تم شحن الطلب، قم بالتواصل مع شركة الشحن لمتابعته";
                }
                return RedirectToAction("OrderFollowing_Result", new { OrderStatusMessage = OrderStatusMessage, OrderID = OrderID });
            }



        }
        [HttpGet]
        public ActionResult OrderFollowing()
        {
            ViewBag.NoOrders = "False";
            return View(db.Orders.ToList());
        }
        [ActionName("OrderFollowing_Result")]
        public ActionResult OrderFollowing(string OrderStatusMessage, int OrderID)
        {
            ViewBag.OrderStatusMessage = OrderStatusMessage;
            if (OrderID != 0)
            {
                ViewBag.NoOrders = "False";
                var Model = db.Orders.Where(c => c.OrderID == OrderID).ToList();
                return View("OrderFollowing", Model);
            }
            else
            {
                ViewBag.NoOrders = "True";
                return View("OrderFollowing");
            }

        }

    }
}
