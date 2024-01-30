using Library_management2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Library_management2.Controllers
{
    public class BorrowController : Controller
    {
        static int userId;          // Used to store user id.
        static string userName;     // Used to store user name.

        private LibraryManagementEntities1 userDb = new LibraryManagementEntities1();


        // Returns user books borrow view, here user can request for a book.
        public ActionResult Index(int? userId, string userName)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDb.Users.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }

            BorrowController.userId = (int)userId;
            BorrowController.userName = userName;
            return View(userDb.Books.ToList());
        }

        // Returns user home view.
        public ActionResult UserHome()
        {
            return View();
        }

        // Returns user about view.
        public ActionResult UserAbout()
        {
            return View();
        }

        // Returns user contact view.
        public ActionResult UserContact()
        {
            return View();
        }

        // Navbar menus.
        // Redirected to index view of borrow controller with user id and username.
        public ActionResult MenuBorrow()
        {
            return RedirectToAction("Index", "Borrow", new { userId = userId, userName = userName });
        }

        // Redirected to Requested view of user transaction controller with user id.
        public ActionResult MenuRequested()
        {
            return RedirectToAction("Requested", "UserTrans", new { userId = userId });
        }

        // Redirected to Received view of user transaction controller with user id.
        public ActionResult MenuReceived()
        {
            Session.Remove("receivedBadge");
            return RedirectToAction("Received", "UserTrans", new { userId = userId });
        }

        // Redirected to Rejected view of user transaction controller with user id.
        public ActionResult MenuRejected()
        {
            Session.Remove("rejectedBadge");
            return RedirectToAction("Rejected", "UserTrans", new { userId = userId });
        }

        // Borrow the book, redirect to index view.
        public ActionResult Borrow(int? bookId)
        {
            /*try
            {*/
            if (userDb.Transactions.Where(t => t.UserID == userId).Count() < 6)
            {
                if (bookId != null)
                {
                    Book book = userDb.Books.FirstOrDefault(b => b.BookID == bookId);
                    if (book == null)
                    {
                        return HttpNotFound();
                    }
                    if (book.BookCopies > 0)
                    {
                        book.BookCopies = book.BookCopies - 1;
                        Guid guid = Guid.NewGuid();
                        byte[] bytes = guid.ToByteArray();
                        Transaction trans = new Transaction()
                        {
                            TransID = BitConverter.ToInt32(bytes, 0),
                            BookID = book.BookID,
                            BookTitle = book.BookTitle,
                            TansDate = DateTime.Now,
                            TransStatus = "Requested",
                            UserID = userId,
                            UserName = userName,
                        };
                        userDb.SaveChanges();
                        userDb.Transactions.Add(trans);
                        userDb.SaveChanges();
                        Session["requestMsg"] = "Requested successfully";
                    }
                    else
                    {
                        Session["requestMsg"] = "Sorry you cant take, Book copy is zero";
                    }
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                Session["requestMsg"] = "Sorry you cant take more than six books";
            }
            return RedirectToAction("Index", "Borrow", new { userId = userId, userName = userName });
            /*}
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
        }

        // Remove the session datas which are used for alerts
        // ReqAlert
        public ActionResult RequestAlert()
        {
            Session.Remove("requestMsg");
            return RedirectToAction("Index", "Borrow", new { userId = userId, userName = userName });
        }
    }
}