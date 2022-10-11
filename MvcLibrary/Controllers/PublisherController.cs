using MvcLibrary.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLibrary.Controllers
{
    public class PublisherController : Controller
    {
        DbLibraryEntities2 dbLibraryEntities1 = new DbLibraryEntities2();
        public ActionResult Index()
        {
            var publishers = dbLibraryEntities1.Tbl_Publisher.ToList();

            return View(publishers);
        }
        [HttpGet]
        public ActionResult AddPublisher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPublisher(Tbl_Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return View("publisher");
            }

            dbLibraryEntities1.Tbl_Publisher.Add(publisher);
            dbLibraryEntities1.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult DeletePublisher(Tbl_Publisher publisher)
        {
            var values = dbLibraryEntities1.Tbl_Publisher.Find(publisher.ID);
            if (values.Status == true) values.Status = false;
            else values.Status = true;
            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetPublisher(Tbl_Publisher publisher)
        {
            var values = dbLibraryEntities1.Tbl_Publisher.Find(publisher.ID);
            return View("GetPublisher", values);
        }
        public ActionResult UpdatePublisher(Tbl_Publisher publisher)
        {
            var values = dbLibraryEntities1.Tbl_Publisher.Find(publisher.ID);

            values.Name = publisher.Name;
           

            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}