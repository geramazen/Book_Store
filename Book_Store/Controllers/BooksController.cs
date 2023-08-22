﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Book_Store.Models;
using System.IO;
using PagedList;

namespace Book_Store.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookContext db = new BookContext();
        public static List<Book> UserCart = new List<Book>();

        // GET: Books
        public ActionResult Index(int? page, string BookName, int? PublisherName, int? CategoryName, int? AutherName)
        {
            SetDropDown();
            var books = db.Books.Include(b => b.Author).Include(b => b.Category).ToList();

            if (BookName != null || PublisherName != null || CategoryName != null || AutherName != null)
            {

                if (BookName != null)
                {
                    books = books.Where(c => c.title.Contains(BookName)).ToList();
                }
                if (PublisherName != null)
                {
                    books = books.Where(p => p.PID == PublisherName.Value).ToList();
                }
                if (CategoryName != null)
                {
                    books = books.Where(p => p.CID == CategoryName.Value).ToList();
                }
                if (AutherName != null)
                {
                    books = books.Where(p => p.AID == AutherName.Value).ToList();
                }
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(books.ToPagedList(pageNumber, pageSize));
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
            ViewBag.PID = new SelectList(db.publishers, "PID", "PName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book, HttpPostedFileBase imgfile)
        {
            if (ModelState.IsValid)
            {
                if (imgfile.FileName.Length > 0)
                {
                    string path = "~/images/" + Path.GetFileName(imgfile.FileName);
                    imgfile.SaveAs(Server.MapPath(path));

                    book.image = path;
                    book.NumberOfRates = 0;
                    book.Rate = 0;
                    db.Books.Add(book);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("img", "Please Select an Image For the book");
                }


            }

            ViewBag.AID = new SelectList(db.Authors, "AID", "FName", book.AID);
            ViewBag.CID = new SelectList(db.Categories, "CID", "CName", book.CID);
            ViewBag.PID = new SelectList(db.publishers, "PID", "PName");
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
            ViewBag.PID = new SelectList(db.publishers, "PID", "PName");
            return View(book);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book, HttpPostedFileBase imgfile = null)
        {
            //var model = db.Books.Where(b => b.ID == book.ID).FirstOrDefault();
            //book.image = model.image;
           
            if (imgfile != null && imgfile.FileName.Length > 0)
            {
                string path = "~/images/" + Path.GetFileName(imgfile.FileName);
                imgfile.SaveAs(Server.MapPath(path));
                book.image = path;
            }

            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AID = new SelectList(db.Authors, "AID", "FName", book.AID);
            ViewBag.CID = new SelectList(db.Categories, "CID", "CName", book.CID);
            ViewBag.PID = new SelectList(db.publishers, "PID", "PName");
            return View(book);
        }


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

        public ActionResult ViewBooks(string Message, string OrderID, int? page, string BookName, int? PublisherName, int? CategoryName, int? AutherName)
        {
            SetDropDown();
            if (Message != null)
            {
                ViewBag.Message = Message;
                ViewBag.OrderID = OrderID;
            }
            var recs = db.Books.Where(c => c.AvailableCopies != 0).ToList();

            if (BookName != null || PublisherName != null || CategoryName != null || AutherName != null)
            {

                if (BookName != null)
                {
                    recs = recs.Where(c => c.title.Contains(BookName)).ToList();
                }
                if (PublisherName != null)
                {
                    recs = recs.Where(p => p.PID == PublisherName.Value).ToList();
                }
                if (CategoryName != null)
                {
                    recs = recs.Where(p => p.CID == CategoryName.Value).ToList();
                }
                if (AutherName != null)
                {
                    recs = recs.Where(p => p.AID == AutherName.Value).ToList();
                }
            }
            int pageSize = 12;
            int pageNumber = (page ?? 1);

            if (recs.Count() == 0)
            {
                ViewBag.NoResult = "عفوا، لا توجد نتائج تطابق هذا البحث";
            }
           
            return View(recs.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult AddToCart(int id)
        {
            Book book = db.Books.Find(id);
            UserCart.Add(new Book
            {
                ID = book.ID,
                title = book.title,
                AID = book.AID,
                description = book.description,
                image = book.image,
                Price = book.Price,
                Publisher = book.Publisher
            });
            Session["Cart"] = UserCart.Count();
            return RedirectToAction("ViewBooks");
        }

        public ActionResult ViewCart(string Message)
        {
            if (Message != null)
            {
                ViewBag.Message = Message;
            }
            var books = UserCart;
            return View(books.ToList());
        }

        public ActionResult UserConfirmOrder(Order order)
        {
            var Books = UserCart;
            return RedirectToAction("Create", "Orders", new { Books, order });
        }

        public ActionResult DeleteFromCart(int id)
        {
            //var todelete = UserCart.Where(c => c.ID == id).FirstOrDefault();
            UserCart.RemoveAt(id);
            Session["Cart"] = UserCart.Count();
            return RedirectToAction("ViewCart", new { Message = "تم الحذف من السلة" });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrder(List<Book> Books, Order order)
        {
            DiscountCoupon Coupon = new DiscountCoupon();
            Book book = new Book();
            Books = UserCart;
            var OrderId = 1;
            if (db.Orders.Count() != 0)
            {
                OrderId = db.Orders.Select(c => c.OrderID).Max() + 1;
            }

            if(order.DiscountCoupon != null)
            {
                Coupon = db.DiscountCoupons.Where(c => c.Name == order.DiscountCoupon).FirstOrDefault();
                if(Coupon != null)
                {
                    var Discount = Coupon.percentage;
                    foreach(var Item in Books)
                    {
                        Item.Price = (Item.Price - ((Discount / 100) * Item.Price));
                    }
                }
            }

            foreach (var item in Books)
            {
                book = db.Books.Where(b => b.ID == item.ID).FirstOrDefault();
                book.AvailableCopies--;
                Order DBorder = new Order
                {
                    Adress = order.Adress,
                    Amount = item.Price,
                    BookName = item.title,
                    OrderID = OrderId,
                    MobileNumber = order.MobileNumber,
                    Name = order.Name,
                    Order_Status = 0,
                    PublisherName = item.Publisher.PName
                };
                if(Coupon != null)
                {
                    DBorder.DiscountCoupon = Coupon.Name;
                }
                db.Orders.Add(DBorder);
            }
            db.SaveChanges();
            Session["Cart"] = 0;
            UserCart.Clear();
            return RedirectToAction("ViewBooks", "Books", new { Message = "تم إرسال طلبك", OrderID = OrderId.ToString() });
        }

        public void SetDropDown()
        {
            ViewData["AutherNameList"] = new SelectList(db.Authors.ToList(), "AID", "FName");
            ViewData["CategoryNameList"] = new SelectList(db.Categories.ToList(), "CID", "CName");
            ViewData["PublisherNameList"] = new SelectList(db.publishers.ToList(), "PID", "PName");
        }

        public decimal AddRate(int Rate, int BID)
        {
            if (Session["UserID"] == null)
            {
                return -1;
            }
            var UID = Convert.ToInt32(Session["UserID"]);
            var CheckRate = db.UsersRates.Where(c => c.UID == UID && c.BID == BID).FirstOrDefault();
            if(CheckRate != null)
            {
                return 0;
            }
            var Book = db.Books.Where(c => c.ID == BID).FirstOrDefault();
            if(Book.NumberOfRates.Value == 0)
            {
                Book.NumberOfRates = 1;
                Book.Rate = Rate;
            }
            else
            {
                var NumberOfRates = Book.NumberOfRates.Value + 1;
                Book.Rate = (Rate + Book.Rate.Value) / 2;
                Book.NumberOfRates = NumberOfRates;
            }
            UsersRate UserRate = new UsersRate()
            {
                BID = BID,
                UID = UID,
                Rate = Rate
            };
            db.UsersRates.Add(UserRate);
            db.SaveChanges();

            return Book.Rate.Value;
        }
    }
}
