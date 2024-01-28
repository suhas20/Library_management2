using Library_management2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_management2.Controllers
{
    public class HomeController : Controller
    {
        private LibraryManagementEntities1 db = new LibraryManagementEntities1();
        // returns home view
        public ActionResult Home()
        {
            return View(db.Books.ToList());
        }

        //returns about view
        public ActionResult About()
        {
            return View();
        }

        //returns contact view
        public ActionResult Contact()
        {
            return View();
        }

        //returns login view
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Index()
        {
            var books = db.Books.ToList();
            return View(books);
        }
    }
}
