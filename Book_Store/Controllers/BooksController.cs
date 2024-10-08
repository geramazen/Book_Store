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
        //static Guid myuid = Guid.NewGuid();
        //string myuidAsString = myuid.ToString();
        public static List<Book> UserCart = new List<Book>();

        // GET: Books
        [Helpers.AdminAccess]
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
            book.WatchersCount++;
            db.SaveChanges();
            return View(book);
        }

        // GET: Books/Create
        [Helpers.AdminAccess]
        public ActionResult Create()
        {
            ViewBag.AID = new SelectList(db.Authors, "AID", "FName");
            ViewBag.CID = new SelectList(db.Categories, "CID", "CName");
            ViewBag.PID = new SelectList(db.publishers, "PID", "PName");
            var BookstatusList = new List<SelectListItem> { new SelectListItem { Text = "جديد", Value = "1" }, new SelectListItem { Text = "مستعمل", Value = "2" } };
            ViewBag.BookStaus = new SelectList(BookstatusList.ToList(), "Value", "text");
            Book book = new Book
            {
                EntryDate = DateTime.Now.Date
            };
            return View(book);
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Helpers.AdminAccess]
        public ActionResult Create(Book book, HttpPostedFileBase imgfile)
        {
            if (ModelState.IsValid)
            {
                if (imgfile != null && imgfile.FileName.Length > 0)
                {
                    string path = "~/images/" + DateTime.Now.ToString("yyyy-MM-dd-H-mm-ss") + Path.GetFileName(imgfile.FileName);
                    imgfile.SaveAs(Server.MapPath(path));

                    book.image = path;
                    book.NumberOfRates = 0;
                    book.Rate = 0;
                    book.EntryDate = DateTime.Now.Date;
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
        [Helpers.AdminAccess]
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
            ViewBag.PID = new SelectList(db.publishers, "PID", "PName", book.PID);
            var BookstatusList = new List<SelectListItem> { new SelectListItem { Text = "جديد", Value = "1", Selected = book.BookStaus == 1 }, new SelectListItem { Text = "مستعمل", Value = "2", Selected = book.BookStaus == 2 } };
            ViewBag.BookStaus = new SelectList(BookstatusList.ToList(), "Value", "text", book.BookStaus);
            return View(book);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Helpers.AdminAccess]
        public ActionResult Edit(Book book, HttpPostedFileBase imgfile)
        {
            //var model = db.Books.Where(b => b.ID == book.ID).FirstOrDefault();
            //book.image = model.image;

            if (imgfile != null && imgfile.FileName.Length > 0)
            {
                string path = "~/images/" + DateTime.Now.ToString("yyyy-MM-dd-H-mm-ss") + Path.GetFileName(imgfile.FileName);
                imgfile.SaveAs(Server.MapPath(path));
                book.image = path;
            }
            //else
            //{
            //    book.description = "imgfile not working";
            //}

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




        // POST: Books/Delete/5
        [Helpers.AdminAccess]
        public ActionResult Delete(int id)
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

        public ActionResult ViewBooks(string Message, string OrderID, int? page, string BookName, int? PublisherName, int? CategoryName, int? AutherName, int? Sort, string Type)
        {
            SetDropDown();
            if (Message != null)
            {
                if (Type == "Tittle")
                {
                    ViewBag.Tittle = "True";
                }
                ViewBag.Message = Message;
                ViewBag.OrderID = OrderID;
            }
            var recs = db.Books.OrderByDescending(c => c.WatchersCount).ToList();

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

            if (Sort != null)
            {
                if (Sort.Value == 1)
                {
                    recs = recs.OrderByDescending(c => c.Rate).ToList();
                }
                else if (Sort.Value == 2)
                {
                    recs = recs.OrderBy(c => c.Price).ToList();
                }
                else if (Sort.Value == 3)
                {
                    recs = recs.OrderByDescending(c => c.EntryDate).ToList();
                }
                else
                {
                    recs = recs.OrderByDescending(c => c.WatchersCount).ToList();
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

        public JsonResult AddToCart(int id)
        {
            if (Session["UserCart"] == null)
            {
                Session["UserCart"] = new List<Book>();
            }
            var UserBooks = (List<Book>)Session["UserCart"];
            Book book = db.Books.Find(id);

            var AvlCop = UserBooks.Where(c => c.ID == id).Count();

            if (AvlCop >= book.AvailableCopies)
            {
                return Json(new { status = false, message = "لقد تجاوزت عدد النسخ المتوفرة من هذا الكتاب" }, JsonRequestBehavior.AllowGet);
            }

            UserBooks.Add(new Book
            {
                ID = book.ID,
                title = book.title,
                AID = book.AID,
                description = book.description,
                image = book.image,
                Price = book.Price,
                Publisher = book.Publisher
            });
            Session["Cart"] = UserBooks.Count();
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewCart(string Message, string copoun)
        {
            var UserBooks = (List<Book>)Session["UserCart"];
            if (Message != null)
            {
                ViewBag.Message = Message;
            }

            if (copoun != null && copoun != "")
            {
                Book Book = new Book();
                var Cop = db.DiscountCoupons.Where(c => c.Name == copoun).FirstOrDefault();
                if (Cop == null)
                {
                    ViewBag.Message = "الكود الذي أدخلته غير صحيح";
                }
                else
                {
                    var Discount = Cop.percentage;
                    foreach (var Item in UserBooks)
                    {
                        Book = db.Books.Where(c => c.ID == Item.ID).FirstOrDefault();
                        Item.Price = (Book.Price - ((Discount / 100) * Book.Price));
                    }
                    ViewBag.Sale = "Sale";
                }
            }
            else
            {
                if (UserBooks != null)
                {
                    Book Book = new Book();
                    foreach (var Item in UserBooks)
                    {
                        Book = db.Books.Where(c => c.ID == Item.ID).FirstOrDefault();
                        Item.Price = Book.Price;
                    }
                }
                else
                {
                    UserBooks = new List<Book>();
                }
            }

            var books = UserBooks;
            return View(books.ToList());
        }

        public ActionResult UserConfirmOrder(Order order)
        {
            var UserBooks = (List<Book>)Session["UserCart"];
            //var Books = UserCart;
            return RedirectToAction("Create", "Orders", new { UserBooks, order });
        }

        public ActionResult DeleteFromCart(int id)
        {
            var UserBooks = (List<Book>)Session["UserCart"];
            var Book = UserBooks.Where(c => c.ID == id).FirstOrDefault();
            UserBooks.Remove(Book);
            Session["Cart"] = UserBooks.Count();
            return RedirectToAction("ViewCart", new { Message = "تم الحذف من السلة" });

        }

        [Helpers.AdminAccess]
        public ActionResult CopyPayed(int id)
        {
            var Book = db.Books.Where(c => c.ID == id).FirstOrDefault();
            Book.AvailableCopies--;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreateOrder(List<Book> Books, Order order)
        {
            var userid = -1;
            if (Session["UserID"] != null)
            {
                userid = int.Parse(Session["UserID"].ToString());
            }

            var UserBooks = (List<Book>)Session["UserCart"];
            if (UserBooks == null)
            {
                UserBooks = new List<Book>();
            }
            if (UserBooks.Count() == 0)
            {
                var BID = int.Parse(Session["BID"].ToString());
                Session["Order"] = order;
                return RedirectToAction("BuyNow", new { BID = BID });
            }
            DiscountCoupon Coupon = new DiscountCoupon();
            Book book = new Book();
            Books = UserBooks;

            foreach (var item in Books)
            {
                item.Price = db.Books.Where(c => c.ID == item.ID).Select(c => c.Price).FirstOrDefault();
            }

            var OrderId = 1;
            if (db.Orders.Count() != 0)
            {
                OrderId = db.Orders.Select(c => c.OrderID).Max() + 1;
            }

            if (order.DiscountCoupon != null)
            {
                Coupon = db.DiscountCoupons.Where(c => c.Name == order.DiscountCoupon).FirstOrDefault();
                if (Coupon != null)
                {
                    var Discount = Coupon.percentage;
                    foreach (var Item in Books)
                    {
                        Item.Price -= (Discount / 100) * Item.Price;
                    }
                }
            }

            foreach (var item in Books)
            {
                book = db.Books.Where(b => b.ID == item.ID).FirstOrDefault();
                if (book.AvailableCopies != 0)
                {
                    book.AvailableCopies--;
                }
                else
                {
                    return RedirectToAction("ViewBooks", "Books", new { Message = "عذرا، لقد نفذت النسخ المتوفرة من كتاب " + item.title });
                }
                Order DBorder = new Order
                {
                    Adress = order.Adress,
                    Amount = item.Price,
                    BookName = item.title,
                    OrderID = OrderId,
                    MobileNumber = order.MobileNumber,
                    Name = order.Name,
                    Order_Status = 0,
                    PublisherName = item.Publisher.PName,
                    OrderDate = DateTime.Today,
                    UserID=userid,                  
                };

                if (Coupon != null)
                {
                    DBorder.DiscountCoupon = Coupon.Name;
                }
                db.Orders.Add(DBorder);
            }
            db.SaveChanges();
            Session["Cart"] = 0;
            UserBooks.Clear();
            return RedirectToAction("ViewBooks", "Books", new { Message = "تم إرسال طلبك", OrderID = OrderId.ToString() });
        }

        public ActionResult BuyNow(int BID)
        {
            var order = (Order)Session["Order"];
            DiscountCoupon Coupon = new DiscountCoupon();
            var OrderId = 1;
            if (db.Orders.Count() != 0)
            {
                OrderId = db.Orders.Select(c => c.OrderID).Max() + 1;
            }

            var book = db.Books.Where(b => b.ID == BID).FirstOrDefault();
            if (book.AvailableCopies != 0)
            {
                book.AvailableCopies--;
            }
            else
            {
                return RedirectToAction("ViewBooks", "Books", new { Message = "عذرا، لقد نفذت النسخ المتوفرة من كتاب " + book.title });
            }

            var Price = book.Price;

            if (order.DiscountCoupon != null)
            {
                Coupon = db.DiscountCoupons.Where(c => c.Name == order.DiscountCoupon).FirstOrDefault();
                if (Coupon != null)
                {
                    var Discount = Coupon.percentage;
                    Price -= (Discount / 100) * book.Price;

                }
            }

            Order DBorder = new Order
            {
                Adress = order.Adress,
                Amount = Price,
                BookName = book.title,
                OrderID = OrderId,
                MobileNumber = order.MobileNumber,
                Name = order.Name,
                Order_Status = 0,
                PublisherName = book.Publisher.PName,
                OrderDate = DateTime.Now.Date
            };
            if (Coupon != null)
            {
                DBorder.DiscountCoupon = Coupon.Name;
            }
            db.Orders.Add(DBorder);
            db.SaveChanges();
            return RedirectToAction("ViewBooks", "Books", new
            {
                Message = "تم إرسال طلبك",
                OrderID = OrderId.ToString()
            });
        }
        public void SetDropDown()
        {
            ViewData["AutherNameList"] = new SelectList(db.Authors.ToList(), "AID", "FName");
            ViewData["CategoryNameList"] = new SelectList(db.Categories.ToList(), "CID", "CName");
            ViewData["PublisherNameList"] = new SelectList(db.publishers.ToList(), "PID", "PName");
            var BookstatusList = new List<SelectListItem> { new SelectListItem { Text = "جديد", Value = "1" }, new SelectListItem { Text = "مستعمل", Value = "2" } };
            ViewData["BookStatus"] = new SelectList(BookstatusList.ToList(), "Value", "text");

        }

        public decimal AddRate(int Rate, int BID)
        {
            if (Session["UserID"] == null)
            {
                return -1;
            }
            var UID = Convert.ToInt32(Session["UserID"]);
            var CheckRate = db.UsersRates.Where(c => c.UID == UID && c.BID == BID).FirstOrDefault();
            if (CheckRate != null)
            {
                return 0;
            }
            var Book = db.Books.Where(c => c.ID == BID).FirstOrDefault();
            if (Book.NumberOfRates.Value == 0)
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

        [HttpPost]
        public ActionResult AddCopies(int BookID, int NumOfCopies)
        {
            var Book = db.Books.Where(c => c.ID == BookID).FirstOrDefault();
            Book.AvailableCopies = Book.AvailableCopies + NumOfCopies;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public class AddCopiesData
        {
            public int BookID { get; set; }
            public int NumOfCopies { get; set; }
        }
    }
}
