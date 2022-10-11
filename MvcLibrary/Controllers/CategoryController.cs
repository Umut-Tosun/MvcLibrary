using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcLibrary.Models.Entity;
namespace MvcLibrary.Controllers
{
    public class CategoryController : Controller
    {
        DbLibraryEntities2 dbLibraryEntities1 = new DbLibraryEntities2();
        public ActionResult Index()
        {
            var values = dbLibraryEntities1.Tbl_Categories.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Tbl_Categories categories)
        {
            dbLibraryEntities1.Tbl_Categories.Add(categories);
            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCategory(Tbl_Categories categories)
        {
            var values = dbLibraryEntities1.Tbl_Categories.Find(categories.ID);
            if (values.Status == true) values.Status = false;
            else values.Status = true;
            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetCategory(Tbl_Categories categories)
        {
            var values = dbLibraryEntities1.Tbl_Categories.Find(categories.ID);
            return View("GetCategory",values);
        }
        public ActionResult UpdateCategory(Tbl_Categories categories)
        {
            var values = dbLibraryEntities1.Tbl_Categories.Find(categories.ID);
            values.Name = categories.Name;
            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}