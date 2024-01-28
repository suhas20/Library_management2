using Library_management2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Library_management2.Controllers
{
    public class UserTransController : Controller
    {
        static int userId;      // Used to store user id

        LibraryManagementEntities1 db = new LibraryManagementEntities1();

        // Returns user requested view, here user can cancel request.
        public ActionResult Requested(int? userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            UserTransController.userId = (int)userId;
            var requestList = db.Transactions.Where(s => s.TransStatus == "Requested" && s.UserID == userId);
            if (requestList.Count() == 0)
            {
                Session["requestMessage"] = "Your Requested list is empty, Go to Borrow section for request a book.";
            }
            else
            {
                Session.Remove("requestMessage");
            }
            return View(requestList.ToList());
        }

        // Cancel book request, redirected to requested
        public ActionResult DeleteRequest(int? tranId)
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
            return RedirectToAction("Requested", "UserTransaction", new { userId = userId });
            /* }
             catch (Exception)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }*/
        }

        // Returns user rejected view, here user can rerequest and cancel book request.
        public ActionResult Rejected(int? userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            UserTransController.userId = (int)userId;
            var rejectedList = db.Transactions.Where(s => s.TransStatus == "Rejected" && s.UserID == userId);
            if (rejectedList.Count() == 0)
            {
                Session["rejectMessage"] = "Your Rejected list is empty, Wait for the admin to take action.";
            }
            else
            {
                Session.Remove("rejectMessage");
            }
            return View(rejectedList.ToList());
        }

        // Rerequest book request, redirected to rejected
        public ActionResult RerequestRejected(int? tranId)
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
            transaction.TransStatus = "Requested";
            transaction.TansDate = DateTime.Now;
            Book book = db.Books.FirstOrDefault(b => b.BookID == transaction.BookID);
            book.BookCopies = book.BookCopies - 1;
            db.SaveChanges();
            db.SaveChanges();
            return RedirectToAction("Rejected", "UserTransaction", new { userId = userId });
            /*}
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
        }

        // Cancel book request, redirected to rejected
        public ActionResult CancelRejected(int? tranId)
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
            return RedirectToAction("Rejected", "UserTransaction", new { userId = userId });
            /* }
             catch (Exception)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }*/
        }

        // Returns user received view, here user can read and return the book, redirected to received
        public ActionResult Received(int? userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            UserTransController.userId = (int)userId;
            var receivedList = db.Transactions.Where(s => s.TransStatus == "Accepted" && s.UserID == userId);
            if (receivedList.Count() == 0)
            {
                Session["receiveMessage"] = "Your Received list is empty, Wait for the admin to take action.";
            }
            else
            {
                Session.Remove("receiveMessage");
            }
            return View(receivedList.ToList());
        }

        // Return book
        public ActionResult ReturnReceived(int? tranId)
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
            transaction.TansDate = DateTime.Now;
            transaction.TransStatus = "Returned";
            db.SaveChanges();
            return RedirectToAction("Received", "UserTransaction", new { userId = userId });
            /* }
             catch (Exception)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }*/
        }
    }
}