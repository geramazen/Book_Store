﻿using System;
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
    public class CategoriesController : Controller
    {
        private readonly BookContext db = new BookContext();

        // GET: Categories
        [Helpers.AdminAccess]
        public ActionResult Index(int? page,string Category)
        {
            var recs = db.Categories.ToList();
            if (Category != null)
            {
                recs = recs.Where(c => c.CName.Contains(Category)).ToList();
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);           
            return View(recs.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult UserIndex(int? page, string CategoryName)
        {
            var recs = db.Categories.ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            if (CategoryName != null)
            {
                recs = recs.Where(c => c.CName.Contains(CategoryName)).ToList();
            }

            return View(recs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Categories/Details/5
        [Helpers.AdminAccess]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        [Helpers.AdminAccess]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Helpers.AdminAccess]
        public ActionResult Create([Bind(Include = "CID,CName")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        [Helpers.AdminAccess]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Helpers.AdminAccess]
        public ActionResult Edit([Bind(Include = "CID,CName")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        [Helpers.AdminAccess]
        public ActionResult Delete(int? id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Categories/Delete/5


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //public ActionResult CategoryBooks(int? page, int CID)
        //{
        //    int pageSize = 10;
        //    int pageNumber = (page ?? 1);
        //    var Books = db.Books.Where(b => b.CID == CID).ToList();
        //    return View(Books.ToPagedList(pageNumber, pageSize));
        //}
    }
}
