using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library_management2.Models;

namespace Library_management2.Controllers
{
    public class TransactionsController : Controller
    {
        private LibraryManagementEntities1 db = new LibraryManagementEntities1();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Book).Include(t => t.Transactions1).Include(t => t.Transaction1).Include(t => t.User);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookTitle");
            ViewBag.TransID = new SelectList(db.Transactions, "TransID", "BookTitle");
            ViewBag.TransID = new SelectList(db.Transactions, "TransID", "BookTitle");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransID,BookID,BookTitle,TransStatus,TansDate,UserID,UserName")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookTitle", transaction.BookID);
            ViewBag.TransID = new SelectList(db.Transactions, "TransID", "BookTitle", transaction.TransID);
            ViewBag.TransID = new SelectList(db.Transactions, "TransID", "BookTitle", transaction.TransID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", transaction.UserID);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookTitle", transaction.BookID);
            ViewBag.TransID = new SelectList(db.Transactions, "TransID", "BookTitle", transaction.TransID);
            ViewBag.TransID = new SelectList(db.Transactions, "TransID", "BookTitle", transaction.TransID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", transaction.UserID);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransID,BookID,BookTitle,TransStatus,TansDate,UserID,UserName")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookID = new SelectList(db.Books, "BookID", "BookTitle", transaction.BookID);
            ViewBag.TransID = new SelectList(db.Transactions, "TransID", "BookTitle", transaction.TransID);
            ViewBag.TransID = new SelectList(db.Transactions, "TransID", "BookTitle", transaction.TransID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", transaction.UserID);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
