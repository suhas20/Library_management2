﻿using Library_management2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Library_management2.Controllers
{
    public class AdminTransController : Controller
    {
        private LibraryManagementEntities1 db = new LibraryManagementEntities1();

        // Returns admin request view, here admin can accept and reject the book requests
        public ActionResult Requests()
        {
            return View(db.Transactions.ToList());
        }
        // Returns all book requests in json format.
        public ActionResult GetAllRequests()
        {
            var transactionList = db.Transactions.Where(r => r.TransStatus == "Requested").ToList();
            return Json(new { data = transactionList }, JsonRequestBehavior.AllowGet);
        }
        // Accepts the book request.
        public ActionResult AcceptRequest(int? tranId)
        {
            /* try
             {*/
            if (tranId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.FirstOrDefault(t => t.TransID == tranId);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            transaction.TransStatus = "Accepted";
            transaction.TansDate = DateTime.Now;
            db.SaveChanges();
            return View("Requests");
            /*}
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/

        }
        // Reject the book request. 
        public ActionResult RejectRequest(int? tranId)
        {
            /*try
            {*/
            if (tranId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.FirstOrDefault(t => t.TransID == tranId);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            transaction.TransStatus = "Rejected";
            transaction.TansDate = DateTime.Now;
            Book book = db.Books.FirstOrDefault(b => b.BookID == transaction.BookID);
            book.BookCopies = book.BookCopies + 1;
            db.SaveChanges();
            db.SaveChanges();
            return View("Requests");
            /*}
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
        }
        // Returns admin accepted view, here admin can view the accepted books.
        public ActionResult Accepted()
        {
            return View(db.Transactions.ToList());
        }
        // Returns all accepted books in json format.
        public ActionResult GetAllAccepted()
        {
            var transactionList = db.Transactions.Where(r => r.TransStatus == "Accepted").ToList();
            return Json(new { data = transactionList }, JsonRequestBehavior.AllowGet);
        }
        // Returns admin return view, here admin can accept book return requests.
        public ActionResult Return()
        {
            return View(db.Transactions.ToList());
        }
        // Returns all return books in json format.
        public ActionResult GetAllReturn()
        {
            var transactionList = db.Transactions.Where(r => r.TransStatus == "Returned").ToList();
            return Json(new { data = transactionList }, JsonRequestBehavior.AllowGet);
        }
        // Accepts the book return request.
        public ActionResult AcceptReturn(int? tranId)
        {

            /*try
            {*/
            if (tranId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.FirstOrDefault(t => t.TransID == tranId);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            Book book = db.Books.FirstOrDefault(b => b.BookID == transaction.BookID);
            book.BookCopies = book.BookCopies + 1;
            db.SaveChanges();
            db.Transactions.Remove(transaction);
            db.SaveChanges();
            return View("Return");
            /*}
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
        }
        // Returns admin home view.
        public ActionResult AdminHome()
        {
            return View();
        }
        // Returns admin about view.
        public ActionResult AdminAbout()
        {
            return View();
        }
        // Returns admin contact view.
        public ActionResult AdminContact()
        {
            return View();
        }
    }
}