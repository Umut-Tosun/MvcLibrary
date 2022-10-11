using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcLibrary.Models.Class;
using MvcLibrary.Models.Entity;

namespace MvcLibrary.Controllers
{
    [AllowAnonymous]
    public class ShowCaseController : Controller
    {
        DbLibraryEntities2 dbLibraryEntities1 = new DbLibraryEntities2();
        public ActionResult Index()
        {
            Class1 cs = new Class1();
            cs.books = dbLibraryEntities1.Tbl_Book.ToList();
            cs.about = dbLibraryEntities1.Tbl_About.ToList();
            return View(cs);
        }
        [HttpPost]
        public ActionResult Index(Tbl_Contact contact)
        {
            contact.Date = DateTime.Now;
            dbLibraryEntities1.Tbl_Contact.Add(contact);
            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}