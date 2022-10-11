using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcLibrary.Models.Entity;

namespace MvcLibrary.Controllers
{
    public class WriterController : Controller
    {
        DbLibraryEntities2 dbLibraryEntities1 = new DbLibraryEntities2();

        public ActionResult Index()
        {
            var values = dbLibraryEntities1.Tbl_Writer.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddWriter(Tbl_Writer writer)
        {
            if(!ModelState.IsValid)
            {
                return View("AddWriter");
            }
            writer.Status = true;
            dbLibraryEntities1.Tbl_Writer.Add(writer);
            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteWriter(Tbl_Writer writer)
        {
            var values = dbLibraryEntities1.Tbl_Writer.Find(writer.ID);
            if (values.Status == true) values.Status = false;
            else values.Status = true;
            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetWriter(Tbl_Writer writer)
        {
            var values = dbLibraryEntities1.Tbl_Writer.Find(writer.ID);
            return View("GetWriter", values);
        }
        public ActionResult GetWriterDetails(int id)
        {
            var values = dbLibraryEntities1.Tbl_Book.Where(x => x.Tbl_Writer.ID == id).ToList();
            ViewBag.writerFullName = dbLibraryEntities1.Tbl_Writer.Where(x => x.ID == id).Select(x => x.FirstName + " " + x.LastName).FirstOrDefault();
            return View("GetWriterDetails", values);
        }
        
        public ActionResult UpdateWriter(Tbl_Writer writer)
        {
            var values = dbLibraryEntities1.Tbl_Writer.Find(writer.ID);
            values.FirstName = writer.FirstName;
            values.LastName = writer.LastName;
            values.Details = writer.Details;

            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}